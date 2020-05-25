namespace User.Web.UI.Services.Implementations
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using User.Helpers;
    using User.Models;
    using User.Web.UI.Services.Interfaces;

    public class AccountUiService : IAccountUiService
    {
        private readonly IOptions<AppSettings> _appSettings;

        public AccountUiService(IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings;
        }

        public async Task<UserServiceResponse> RegisterUserAsync(UserRegistration user)
        {
            var result = await UserManagementHttpClient
                             .PostApiCallAsync(this._appSettings.Value.BaseAddress, this._appSettings.Value.RegisterUserAddress, user)
                             .ConfigureAwait(true);

            return result;
        }

        public async Task<UserServiceResponse> LoginUserAsync(UserLogin user)
        {
            var result = await UserManagementHttpClient
                             .PostApiCallAsync(this._appSettings.Value.BaseAddress, this._appSettings.Value.LoginUserAddress, user)
                             .ConfigureAwait(true);

            return result;
        }

        public async Task<UserServiceResponse> VerifyUserAsync(UserVerification user)
        {
            var result = await UserManagementHttpClient
                             .PostApiCallAsync(this._appSettings.Value.BaseAddress, this._appSettings.Value.UserVerificationAddress, user)
                             .ConfigureAwait(true);

            return result;
        }
    }
}
