using Microsoft.AspNetCore.Mvc;
using Proje_Odevi.Entities;
using Proje_Odevi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_Odevi.Controllers
{
    public class PolyclinicController : Controller
    {
        private readonly DatabaseContext _databasecontext;
      
        public PolyclinicController(DatabaseContext databasecontext)
        {
            _databasecontext = databasecontext;
        }
        public IActionResult Polyclinic()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PolyclinicAdd(PolyclinicViewModel PolyclinicViewModel)
        {
            if (ModelState.IsValid)
            {
                var newpolyclinic = new PolyclinicViewModel
                {
                    PolyclinicId = PolyclinicViewModel.PolyclinicId,
                    PolyclinicName = PolyclinicViewModel.PolyclinicName
                    
                };

                _databasecontext.Polyclinics.Add(newpolyclinic);
                _databasecontext.SaveChanges();

                return RedirectToAction("Polyclinic"); // Doktorları listeleme sayfasına yönlendirme
            }

            // Eğer model geçerli değilse, aynı sayfayı tekrar göster
            return View(PolyclinicViewModel);
        }

        public IActionResult Polyclinics()
        {
            var polyclinic = _databasecontext.Polyclinics.ToList();
            return View(polyclinic);
        }
    }
}
