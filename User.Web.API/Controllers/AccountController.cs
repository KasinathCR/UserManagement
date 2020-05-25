namespace User.Web.API.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using User.Models;
    using User.Web.API.Services.Interfaces;

    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountApiService _accountApiService;

        public AccountController(IAccountApiService accountApiService)
        {
            this._accountApiService = accountApiService;
        }

        [HttpPost]

        //[ValidateAntiForgeryToken]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(UserRegistration user)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(new { Message = "Invalid Input" });

            var userRegistrationResponse = await this._accountApiService.RegisterUserAsync(user).ConfigureAwait(true);

            if (userRegistrationResponse.IsValidUser)
                return this.Ok(userRegistrationResponse);
            return this.BadRequest();
        }
    }
}
