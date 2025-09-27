using EGHeals.Application.Contracts.Users;
using EGHeals.Application.Dtos.Users;
using EGHeals.Application.Extensions.Users;
using Microsoft.AspNetCore.Identity;

namespace EGHeals.Application.Features.Users.Commands.Login
{
    public class LoginCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher<object> passwordHasher) : ICommandHandler<LoginCommand, EGResponse<UserDto>>
    {
        public async Task<EGResponse<UserDto>> Handle(LoginCommand command, CancellationToken cancellationToken)
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

            //Possibility user table state was changed in between these operations
            //Ensure again the user is exist
            if (user is null)
            {
                throw new BadRequestException("Incorrect username or password");
            }

            var userDto = user.ToUserDto();

            //CREATE TOKEN
            return new EGResponse<UserDto> { Success = true, Data = userDto };
        }
    }
}
