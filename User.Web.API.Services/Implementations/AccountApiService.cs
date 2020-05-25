namespace User.Web.API.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using User.Entities;
    using User.Helpers;
    using User.Models;
    using User.Web.API.Repository.Interfaces;
    using User.Web.API.Services.Interfaces;

    public class AccountApiService : IAccountApiService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountApiService(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        public async Task<UserServiceResponse> RegisterUserAsync(UserRegistration user)
        {
            var response = new UserServiceResponse();

            var existingUser = await this._accountRepository.FindUserByEmailAsync(emailAddress: user.EmailAddress).ConfigureAwait(continueOnCapturedContext: false);

            if (existingUser != null) return response;

            var result = await this._accountRepository.CreateUserAsync(user: user)
                             .ConfigureAwait(continueOnCapturedContext: true);

            if (!result.Succeeded) return response;

            response.IsValidUser = true;

            existingUser = await this._accountRepository.FindUserByEmailAsync(emailAddress: user.EmailAddress)
                               .ConfigureAwait(continueOnCapturedContext: false);

            var emailMessage = EmailComposer.ComposeEmail(existingUser.UserName, existingUser.VerificationCode);

            EmailSender.SendEmail(@"noreply.testuser44@gmail.com", existingUser.Email, @"testuser44", emailMessage);

            return response;
        }

        public async Task<UserServiceResponse> LoginAsync(UserLogin user)
        {
            var response = new UserServiceResponse();

            var result = await this._accountRepository.LoginAsync(user).ConfigureAwait(false);

            if (!result.Succeeded) return response;

            var loginUser = await this._accountRepository.FindUserByEmailAsync(user.EmailAddress).ConfigureAwait(true);

            if (loginUser != null)
            {
                response.IsValidUser = true;
                response.User = ProvideUserInfo(loginUser);
            }
            else
            {
                response.IsValidUser = false;
            }

            return response;
        }

        public async Task<UserServiceResponse> VerifyUserAsync(UserVerification user)
        {
            var response = new UserServiceResponse();

            var loginUser = await this._accountRepository.FindUserByEmailAsync(user.UserName).ConfigureAwait(true);

            if (!loginUser.VerificationCode.Equals(user.VerificationCode)) return response;

            loginUser.IsVerified = true;
            var result = await this._accountRepository.UpdateUserAsync(loginUser).ConfigureAwait(true);
            if (!result.Succeeded) return response;

            response.IsValidUser = true;
            response.User = ProvideUserInfo(loginUser);

            return response;
        }

        private static UserInfo ProvideUserInfo(ApplicationUser user)
        {
            var userInfo = new UserInfo()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ContactNumber = user.PhoneNumber,
                EmailAddress = user.Email,
                EmailNotifications = user.EmailNotifications,
                IsVerified = user.IsVerified,
                Address = user.Address
            };

            return userInfo;
        }
    }
}
