
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proje_Odevi.Entities;
using Proje_Odevi.Models;

namespace Proje_Odevi.Controllers
{
    public class AppointmentController : Controller
    {

        private readonly DatabaseContext _databasecontext;
        private readonly IConfiguration _configuration;

        public AppointmentController(DatabaseContext databasecontext)
        {
            _databasecontext = databasecontext;
        }

        public IActionResult Appointment()
        {
            var doctors = _databasecontext.Doctors.ToList();
            ViewBag.Doctors = new SelectList(doctors, nameof(DoctorViewModel.DoctorName), nameof(DoctorViewModel.DoctorName));

            var polyclinic = _databasecontext.Polyclinics.ToList();
            ViewBag.Polyclinics = new SelectList(polyclinic, nameof(PolyclinicViewModel.PolyclinicName), nameof(PolyclinicViewModel.PolyclinicName));

            var AppointmentViewModel = new AppointmentViewModel
            {
                // Diğer özellikleri burada set edebilirsiniz.
            };

            return View(AppointmentViewModel);
        }
      

        [HttpPost]
        public IActionResult AppointmentAdd(AppointmentViewModel AppointmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var newappointment = new AppointmentViewModel
                {
                    AppointmentId = AppointmentViewModel.AppointmentId,
                    Date = AppointmentViewModel.Date,
                    Policlinic = AppointmentViewModel.Policlinic,
                    HospitalName = AppointmentViewModel.HospitalName,
                    Doctor = AppointmentViewModel.Doctor
                };

                _databasecontext.Appointments.Add(newappointment);
                _databasecontext.SaveChanges();

                return RedirectToAction("Appointment"); // Doktorları listeleme sayfasına yönlendirme
            }

            // Eğer model geçerli değilse, aynı sayfayı tekrar göster
            return View(AppointmentViewModel);
        }

        // Doktorları listeleyen action
        public IActionResult Appointments()
        {
            var appointment = _databasecontext.Appointments.ToList();
            return View(appointment);
        }

        public IActionResult MyAppointment()
        {
            return View();
        }  
  
           
        

    }
}
    

