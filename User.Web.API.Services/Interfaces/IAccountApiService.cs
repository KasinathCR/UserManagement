namespace User.Web.API.Services.Interfaces
{
    using System.Threading.Tasks;
    using User.Models;

    public interface IAccountApiService
    {
        Task<UserServiceResponse> RegisterUserAsync(UserRegistration user);

        Task<UserServiceResponse> LoginAsync(UserLogin user);

        Task<UserServiceResponse> VerifyUserAsync(UserVerification user);
    }
}
