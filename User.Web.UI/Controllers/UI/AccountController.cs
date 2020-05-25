namespace User.Web.Controllers.UI
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;
    using User.Models;
    using User.Web.UI.Services.Interfaces;

    public class AccountController : Controller
    {
        private readonly IAccountUiService _accountUiService;

        public AccountController(IAccountUiService accountUiService)
        {
            this._accountUiService = accountUiService;
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistration user)
        {
            if (!this.ModelState.IsValid)
                return this.View();

            var response = await this._accountUiService.RegisterUserAsync(user).ConfigureAwait(true);

            if (response != null && response.IsValidUser) return this.RedirectToAction($"Login", $"Account");

            this.ModelState.AddModelError(string.Empty, "User Registration Unsuccessful!");
            return this.View(user);
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin user)
        {
            if (!this.ModelState.IsValid)
                return this.View(user);

            var response = await this._accountUiService.LoginUserAsync(user).ConfigureAwait(true);

            if (response != null && response.IsValidUser && response.User != null)
            {
                const string scheme = CookieAuthenticationDefaults.AuthenticationScheme;
                var loginUser = new ClaimsPrincipal(
                    new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, response.User.EmailAddress) }, scheme));

                //await this.HttpContext.SignInAsync(scheme, loginUser).ConfigureAwait(true);
                return this.SignIn(loginUser, scheme);

                //if (!response.User.IsVerified)
                //{
                //    return this.LocalRedirect("/Account/NewUserVerification");
                //}

                //return this.RedirectToAction($"Login", $"Account");
            }

            this.ModelState.AddModelError(string.Empty, "Invalid Login Attempted");
            return this.View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(true);
            return this.RedirectToAction($"Index", $"Home");
        }
    }
}
