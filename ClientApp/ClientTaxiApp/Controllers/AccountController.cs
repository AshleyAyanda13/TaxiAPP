using ClientTaxiApp.Controllers;
using ClientTaxiApp.Models;
using ClientTaxiApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
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
        public IActionResult Register(string ReturnUrl)
        {
         HttpContext.SignOutAsync();
            return View(new RegisterModel  { ReturnUrl=ReturnUrl});
        }


        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            HttpContext.SignOutAsync();
            return View(new LoginModel { ReturnUrl=ReturnUrl });
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
                if (user.UserName == "admin" && user.Email == "admin@TaxiApp.com")
                {

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, "Admin")
                    };

                    var userIdentity = new ClaimsIdentity(claims, "adminLogin");
                    var userPrincipal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync("Cookie", userPrincipal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                        IsPersistent = false,
                        AllowRefresh = false
                    });

                    return Redirect(loginmodel.ReturnUrl ?? "/Home/Index");
                }

                else
                {



                    

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





                

            }

            ModelState.AddModelError("", "Failed to login User");
            return View(loginmodel);

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registermodel)
        {

            var apiUrl = _options.ApiUrl + "/Account/register";
        
            var loginRes = await _httpClient.PostAsJsonAsync(apiUrl, registermodel);

            
            if (loginRes.StatusCode == System.Net.HttpStatusCode.Created)
            {



                return RedirectToAction("Login", "Account");
            }
            else
            {

                ModelState.AddModelError("", "Failed to register");
                return View(registermodel);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ViewAccountView()
        {

         






             



          return View();


        }
        [AllowAnonymous]
     
        public async Task<IActionResult> Logout()
        {



            var apiUrl = _options.ApiUrl + "/Account/logout";


            var requestContent = new StringContent("", Encoding.UTF8, "application/json");
            var loginRes =await _httpClient.PostAsync(apiUrl,requestContent);


            if (loginRes.IsSuccessStatusCode)
            {


                await HttpContext.SignOutAsync();

                return RedirectToAction("Login", "Account");

            }
            else
            {


                return RedirectToAction("Index", "Home");
            }

        }






    }
}

