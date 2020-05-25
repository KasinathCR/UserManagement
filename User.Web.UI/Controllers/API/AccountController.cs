namespace User.Web.Controllers.API
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using User.Models;
    using User.Web.API.Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountApiService _accountApiService;

        public AccountController(IAccountApiService accountApiService)
        {
            this._accountApiService = accountApiService;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(UserRegistration user)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var response = await this._accountApiService.RegisterUserAsync(user).ConfigureAwait(true);

            if (response.IsValidUser)
                return this.Ok(response);

            return this.BadRequest();
        }

        [HttpPost]
        [Route("LoginUser")]
        public async Task<IActionResult> LoginUser(UserLogin user)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var response = await this._accountApiService.LoginAsync(user).ConfigureAwait(true);

            if (response.IsValidUser)
                return this.Ok(response);

            return this.BadRequest();
        }

        [HttpPost]
        [Route("VerifyUser")]
        public async Task<IActionResult> VerifyUser(UserVerification user)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var response = await this._accountApiService.VerifyUserAsync(user).ConfigureAwait(true);

            if (response.IsValidUser)
                return this.Ok(response);

            return this.BadRequest();
        }
    }
}
