namespace User.Web.API.Repository.Implementations
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using User.Entities;
    using User.Models;
    using User.Web.API.Repository.Interfaces;

    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<ApplicationUser> FindUserByEmailAsync(string emailAddress)
        {
            var applicationUser = await this._userManager.FindByEmailAsync(emailAddress).ConfigureAwait(true);
            return applicationUser;
        }

        public async Task<IdentityResult> CreateUserAsync(UserRegistration user)
        {
            var applicationUser = new ApplicationUser()
            {
                Email = user.EmailAddress,
                VerificationCode = new Random().Next(0, 9999),
                UserName = user.EmailAddress
            };
            var result = await this._userManager.CreateAsync(applicationUser, user.Password).ConfigureAwait(true);
            return result;
        }

        public async Task<SignInResult> LoginAsync(UserLogin user)
        {
            var result = await this._signInManager.PasswordSignInAsync(
                             user.EmailAddress,
                             user.Password,
                             user.RememberMe,
                             false).ConfigureAwait(true);

            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            var result = await this._userManager.UpdateAsync(user).ConfigureAwait(true);
            return result;
        }
    }
}
