namespace User.Web.Pages.Account
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using User.Models;
    using User.Web.UI.Services.Interfaces;

    public class NewUserVerificationModel : PageModel
    {
        private readonly IAccountUiService _accountUiService;

        public NewUserVerificationModel(IAccountUiService accountUiService)
        {
            this._accountUiService = accountUiService;
        }

        [BindProperty]
        public string VerificationCode { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var isVerificationCodeValid = int.TryParse(this.VerificationCode, out var verificationCode);

            if (!isVerificationCodeValid)
            {
                this.ModelState.AddModelError(string.Empty, "Verification Code Invalid!");
                return this.Page();
            }

            var userVerification = new UserVerification()
            {
                VerificationCode = verificationCode,
                UserName = this.User.Identity.Name
            };

            var response = await this._accountUiService.VerifyUserAsync(userVerification).ConfigureAwait(true);

            if (response != null && response.IsValidUser && response.User != null)
            {
                const string scheme = CookieAuthenticationDefaults.AuthenticationScheme;
                var loginUser = new ClaimsPrincipal(
                    new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userVerification.UserName) }, scheme));
                await this.HttpContext.SignInAsync(loginUser).ConfigureAwait(true);

                return this.RedirectToAction($"Login", $"Account");
            }

            this.ModelState.AddModelError(string.Empty, "Verification Code Mismatch!");
            return this.Page();
        }
    }
}