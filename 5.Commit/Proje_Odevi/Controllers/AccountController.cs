using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using Proje_Odevi.Entities;
using Proje_Odevi.Models;
using System.Reflection;
using System.Security.Claims;

namespace Proje_Odevi.Controllers
{

    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;


        public AccountController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string md5salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                string saltedPassword = model.Password + md5salt;
                string hashedPassword = saltedPassword.MD5();

                User user = _databaseContext.Users.SingleOrDefault(x => x.Username.ToLower() == model.Username.ToLower()
                && x.Password == hashedPassword);

                if(user != null)
                {
                    if (user.Locked)
                    {
                        ModelState.AddModelError(nameof(model.Username), "user is locked ");
                        return View(model);
                    }

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name,user.NameSurname ?? string.Empty));
                    claims.Add(new Claim("Username", user.Username));
   
                    ClaimsIdentity identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal= new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
                    return RedirectToAction("Index", "home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifreniz hatalı.");
                }

            }

            return View(model);
        }

        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            
            if (ModelState.IsValid)
            {

                if(_databaseContext.Users.Any(x=>x.Username.ToLower()== model.Username.ToLower())) 
                
                {
                    ModelState.AddModelError(nameof(model.Username),"Kullanıcı adı mevcuttur");
                    View(model);
                }

                string md5salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                string saltedPassword = model.Password + md5salt;
                string hashedPassword = saltedPassword.MD5();

                User user = new()
                {
                    Username = model.Username,
                    Password = hashedPassword
                };
            
               _databaseContext.Users.Add(user);
               _databaseContext.SaveChanges();
            
            }

            return View(model);

        }
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Logout()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

    }
}
