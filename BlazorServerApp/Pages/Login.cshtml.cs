using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorGoogleOAuth.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public IActionResult OnGetAsync(string returnUrl = "")
        {
            var url = String.IsNullOrWhiteSpace(returnUrl) ? Request.Headers["Referer"].ToString() : Request.Host.ToString();
            returnUrl = String.IsNullOrWhiteSpace(url) ? "/" : new Uri(url).AbsolutePath;

            string provider = "Google";
            // Request a redirect to the external login provider.
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Page("./Login", 
                pageHandler: "Callback", 
                values: new { returnUrl }),
            };
            return new ChallengeResult(provider, authenticationProperties);
        }
        public async Task<IActionResult> OnGetCallbackAsync(
            string returnUrl = "", string remoteError = "")
        {
            // Get the information about the user from the external login provider
            var GoogleUser = this.User.Identities.FirstOrDefault();
            if (GoogleUser.IsAuthenticated)
            {
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = this.Request.Host.Value
                };
                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(GoogleUser),
                authProperties);
            }
            return LocalRedirect(returnUrl);
        }
    }
}
