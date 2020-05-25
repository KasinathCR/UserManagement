namespace User.Web.UI.Services.Interfaces
{
    using System.Threading.Tasks;
    using User.Models;

    public interface IAccountUiService
    {
        Task<UserServiceResponse> RegisterUserAsync(UserRegistration user);

        Task<UserServiceResponse> LoginUserAsync(UserLogin user);

        Task<UserServiceResponse> VerifyUserAsync(UserVerification user);
    }
}
