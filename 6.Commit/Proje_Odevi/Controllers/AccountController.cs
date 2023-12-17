using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using Proje_Odevi.Entities;
using Proje_Odevi.Models;
using System.ComponentModel.DataAnnotations;
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
                string hashedPassword = DoMD5HashedString(model.Password);

                User user = _databaseContext.Users.SingleOrDefault(x => x.Username.ToLower() == model.Username.ToLower()
                && x.Password == hashedPassword);

                if (user != null)
                {
                    if (user.Locked)
                    {
                        ModelState.AddModelError(nameof(model.Username), "user is locked ");
                        return View(model);
                    }

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.NameSurname ?? string.Empty));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));
                    claims.Add(new Claim("Username", user.Username));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifreniz hatalı.");
                }

            }

            return View(model);
        }

        private string DoMD5HashedString(string a)
        {
            string md5salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            string salted = a + md5salt;
            string hashed = salted.MD5();
            return hashed;
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

                string hashedPassword = DoMD5HashedString(model.Password);

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
            ProfileInfoLoader();
            return View();
        }

        private void ProfileInfoLoader()
        {
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);
            ViewData["FullName"] = user.NameSurname;
        }

        [HttpPost]
        public IActionResult ProfileChangeFullName([Required] string fullname)
        {

            if (ModelState.IsValid)
            {
                Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user=_databaseContext.Users.SingleOrDefault(x=>x.Id== userid);
                user.NameSurname = fullname;
                _databaseContext.SaveChanges();
                return RedirectToAction(nameof(Profile));
            }
            ProfileInfoLoader();
            return View("Profile");
        }
        [HttpPost]
        public IActionResult ProfileChangePassword([Required] [MinLength(6,ErrorMessage ="En az 6 haneli şifre girin")] string password)
        {

            if (ModelState.IsValid)
            {
                Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Users.SingleOrDefault(x => x.Id == userid);

                string hashedPassword = DoMD5HashedString(password);
                user.Password = hashedPassword;
                _databaseContext.SaveChanges();
                ViewData["result"] = "Şifre Değiştirildi";

            }
            ProfileInfoLoader();
            return View("Profile");

        }


        public IActionResult Logout()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

    }
}
