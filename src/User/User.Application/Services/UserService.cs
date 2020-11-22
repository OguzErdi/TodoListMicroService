using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ResultTypes;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User.Application.Constants;
using User.Application.Helpers;
using User.Application.Interfaces;
using User.Application.Models;
using User.Core.Entities;
using User.Core.Repositories;

namespace User.Application.Services
{
    public class UserService : IUserService
    {
        private const int MinUsernameLength = 3;
        private readonly AppSettings appSettings;
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserService> logger;

        public UserService(
            IOptions<AppSettings> appSettings,
            IUserRepository userRepository,
            ILogger<UserService> logger)
        {
            this.appSettings = appSettings.Value;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task<IDataResult<UserTokenModel>> LoginAsync(string username, string password)
        {
            var user = await userRepository.GetUserAsync(username);
            if (user == null)
            {
                logger.LogInformation(Messages.UserNotFound);
                return new ErrorDataResult<UserTokenModel>(Messages.UserNotFound);
            }

            var isPasswordCorrect = userRepository.VerifyPassword(user, password);
            if (!isPasswordCorrect)
            {
                logger.LogInformation(Messages.PasswordError);
                return new ErrorDataResult<UserTokenModel>(Messages.PasswordError);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.appSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string generatedToken = tokenHandler.WriteToken(token);

            logger.LogInformation(Messages.AccessTokenCreated);
            return new SuccessDataResult<UserTokenModel>(new UserTokenModel(username, generatedToken), Messages.AccessTokenCreated);
        }

        public async Task<IResult> RegisterAsync(string username, string password, string passworRepeat)
        {
            if (password != passworRepeat)
            {
                logger.LogInformation(Messages.PasswordsDoesntMatch);
                return new ErrorResult(Messages.PasswordsDoesntMatch);
            }

            var isUsernameAlreadyTeken = await userRepository.IsUserExistAsync(username);
            if (isUsernameAlreadyTeken)
            {
                logger.LogInformation(Messages.UserAlreadyExists);
                return new ErrorResult(Messages.UserAlreadyExists);
            }

            var isUsernameShort = username.Length < MinUsernameLength;
            if (isUsernameShort)
            {
                string minCharLengthWarning = string.Format(Messages.UsernameMustBeAtLeast, MinUsernameLength.ToString());
                logger.LogInformation(minCharLengthWarning);
                return new ErrorResult(minCharLengthWarning);
            }

            var userEntity = await userRepository.AddUserAsync(username, password);
            if (!userEntity)
            {
                logger.LogInformation(Messages.UsernameMustBeAtLeast);
                return new ErrorResult(Messages.UsernameMustBeAtLeast);
            }

            logger.LogInformation(Messages.UserRegistered);
            return new SuccessResult(Messages.UserRegistered);
        }

        public async Task<IDataResult<bool>> IsUserExist(string username)
        {
            var isUserExist = await userRepository.IsUserExistAsync(username); 
            if (!isUserExist)
            {
                logger.LogInformation(Messages.UserNotFound);
                return new SuccessDataResult<bool>(false);
            }

            logger.LogInformation(Messages.UserFound);
            return new SuccessDataResult<bool>(true);
        }
    }
}
