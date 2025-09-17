using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using MediatR;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

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
                (string Title, int StatusCode, string Detail) details = exception switch
                {
                    NotFoundException => (exception.GetType().Name, 404, exception.Message),
                    BadRequestException => (exception.GetType().Name, 400, exception.Message),
                    InternalServerErrorException => (exception.GetType().Name, 500, exception.Message),
                    ValidationException => (exception.GetType().Name, 400, exception.Message),
                    SqlException => HandleSqlException((SqlException)exception),
                    _ => (exception.GetType().Name, 500, exception.Message),
                };

                var appException = new AppException(details.Title, details.StatusCode, details.Detail);

                throw appException;
            }
        }
        private (string Title, int StatusCode, string Detail) HandleSqlException(SqlException sqlException)
        {
            return sqlException.Number switch
            {
                2601 or 2627 => ("Unique Constraint Violation", 409, "A record with the same value already exists."), // Unique constraint
                547 => ("Foreign Key Violation",409, "A related record is preventing this operation."), // Foreign key violation
                _ => ("Database Error", 500, $"A database error occurred")
            };
        }
    }
}
