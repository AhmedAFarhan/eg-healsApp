using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BuildingBlocks.Behaviors
{
    public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception exception)
            {
                (string Title, int StatusCode, List<string> Errors) details = exception switch
                {
                    NotFoundException => (exception.GetType().Name, 404, new List<string> { exception.Message }),
                    BadRequestException => (exception.GetType().Name, 400, new List<string> { exception.Message }),
                    InternalServerErrorException => (exception.GetType().Name, 500, new List<string> { exception.Message }),
                    ValidationException => HandleValidationException((ValidationException)exception),
                    SqlException => HandleSqlException((SqlException)exception),
                    _ => (exception.GetType().Name, 500, new List<string> { exception.Message }),
                };

                var appException = new AppException(details.Title, details.StatusCode, details.Errors);

                throw appException;
            }
        }
        private (string Title, int StatusCode, List<string> Errors) HandleValidationException(ValidationException validationException)
        {
            var errors = validationException.Errors.Select(error => $"{error.Severity}").ToList();

            return ("Validation Error", 400, errors);
        }
        private (string Title, int StatusCode, List<string> Errors) HandleSqlException(SqlException sqlException)
        {
            return sqlException.Number switch
            {
                2601 or 2627 => ("Unique Constraint Violation", 409, new List<string> { "A record with the same value already exists." }), // Unique constraint
                547 => ("Foreign Key Violation",409, new List<string> { "A related record is preventing this operation." }), // Foreign key violation
                _ => ("Database Error", 500, new List<string> { "A database error occurred." })
            };
        }
    }
}
