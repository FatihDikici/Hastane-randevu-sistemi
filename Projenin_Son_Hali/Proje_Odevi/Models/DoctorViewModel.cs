using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_Odevi.Models
{
    [Table("Doktorlar")]
    public class DoctorViewModel
    {
        [Key]
        public Guid DoctorId { get; set; }

        public string DoctorName { get; set;}

        public string DoctorBranch { get; set; }
    }
}
