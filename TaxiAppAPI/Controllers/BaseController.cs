using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaxiAppApi.Data;
using TaxiAppApi.Model;

namespace TaxiAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {


        protected readonly AppDbContext _appDbContext;




        protected readonly UserManager<AppUser> _userManager;
        protected readonly SignInManager<AppUser> _signInManager;
        protected readonly RoleManager<IdentityRole> _roleManager;

        public BaseController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public BaseController (AppDbContext appDbContext, UserManager<AppUser> userManager)
        {




            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public BaseController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)




        {




            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;



        }



    }
}
