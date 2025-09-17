using EGHeals.Application.Contracts.Users;
using Microsoft.AspNetCore.Identity;

namespace EGHeals.Application.Features.Users.Commands.Login
{
    public class LoginCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher<object> passwordHasher) : ICommandHandler<LoginCommand, LoginResult>
    {
        public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetCustomRepository<IUserRepository>();

            //CHECK IF USER EXIST
            var userExist = await repo.IsUserExistAsync(command.UserLogin.Username, cancellationToken);
            if (userExist is null || passwordHasher.VerifyHashedPassword(null, userExist.PasswordHash, command.UserLogin.Password) == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Incorrect username or password");
            }

            //GET USER CREDENTIALS
            var user = await repo.GetUserCredentialsAsync(command.UserLogin.Username, cancellationToken);

            //CREATE TOKEN


            return new LoginResult("token");
        }
    }
}
