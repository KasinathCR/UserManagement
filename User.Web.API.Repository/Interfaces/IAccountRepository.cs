namespace User.Web.API.Repository.Interfaces
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using User.Entities;
    using User.Models;

    public interface IAccountRepository
    {
        Task<ApplicationUser> FindUserByEmailAsync(string emailAddress);

        Task<IdentityResult> CreateUserAsync(UserRegistration user);

        Task<SignInResult> LoginAsync(UserLogin user);

        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
    }
}
