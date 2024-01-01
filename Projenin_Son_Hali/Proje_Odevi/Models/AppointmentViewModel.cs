
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_Odevi.Models
{
    [Table("Randevu_Ekleme")]
    public class AppointmentViewModel
    {
        [Key]
        public int AppointmentId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Policlinic { get; set; }
        public string HospitalName { get; set; }
        public string Doctor { get; set; }

    }
}