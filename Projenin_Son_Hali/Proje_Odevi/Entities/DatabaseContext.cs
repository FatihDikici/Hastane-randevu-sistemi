using Microsoft.EntityFrameworkCore;
using Proje_Odevi.Models;

namespace Proje_Odevi.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<DoctorViewModel> Doctors { get; set; }

        public DbSet<AppointmentViewModel> Appointments { get; set; }

        public DbSet<PolyclinicViewModel> Polyclinics {  get; set; }



    }
}
