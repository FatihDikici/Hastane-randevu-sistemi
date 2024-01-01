using Microsoft.AspNetCore.Mvc;
using Proje_Odevi.Entities;
using Proje_Odevi.Models;

namespace Proje_Odevi.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DatabaseContext _databasecontext;

        public DoctorController(DatabaseContext databasecontext)
        {
            _databasecontext = databasecontext;
        }
        public IActionResult Doctor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DoctorAdd(DoctorViewModel DoctorViewModel)
        {
            if (ModelState.IsValid)
            {
                var newdoctor = new DoctorViewModel
                {
                    DoctorId = Guid.NewGuid(),
                    DoctorName = DoctorViewModel.DoctorName,
                    DoctorBranch = DoctorViewModel.DoctorBranch
                };

                _databasecontext.Doctors.Add(newdoctor);
                _databasecontext.SaveChanges();

                return RedirectToAction("Doctor"); // Doktorları listeleme sayfasına yönlendirme
            }

            // Eğer model geçerli değilse, aynı sayfayı tekrar göster
            return View(DoctorViewModel);
        }

        // Doktorları listeleyen action
        public IActionResult Doctors()
        {
            var doctor = _databasecontext.Doctors.ToList();
            return View(doctor);
        }
    }
}
    

