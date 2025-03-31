using ClientTaxiApp.Controllers;
using ClientTaxiApp.Models;
using ClientTaxiApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using TaxiAppClient.Models;

namespace TaxiAppClient.Controllers
{
    [Authorize]
    public class AccountController : BaseWebController
    {
        public AccountController(Options options, HttpClient httpClient) : base(options, httpClient)
        {
        }


        [AllowAnonymous]

        public IActionResult Login(string ReturnUrl)
        {


            return View(new LoginModel { ReturnUrl = ReturnUrl });
        }




        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> Login(LoginModel loginmodel)
        {

            var apiUrl = _options.ApiUrl + "/Account/login";
            await HttpContext.SignOutAsync();

            var loginRes = await _httpClient.PostAsJsonAsync(apiUrl, loginmodel);
            if (loginRes.StatusCode == System.Net.HttpStatusCode.OK)
            {






                var res = await _httpClient.GetStringAsync(apiUrl);

                var user = JsonConvert.DeserializeObject<AppUser>(res);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, "AppUser")

                };


                var userIdentity = new ClaimsIdentity(claims, "userLogin");
                var userPrincipal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync("Cookie", userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false 
                });


                return Redirect(loginmodel.ReturnUrl ?? "/Home/Index");
            }
            ModelState.AddModelError("", "Failed to login User");
            return View(loginmodel);

        }
    }
}

