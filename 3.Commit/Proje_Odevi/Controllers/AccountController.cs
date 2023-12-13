using Microsoft.AspNetCore.Mvc;
using Proje_Odevi.Models;
using System.Reflection;

namespace Proje_Odevi.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //login işlemleri
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
                //Register işlemleri
            }

            return View(model);

        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}
