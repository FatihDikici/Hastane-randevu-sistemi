using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_Odevi.Models
{
    [Table("Poliklinik_Ekleme")]
    public class PolyclinicViewModel
    {
        [Key]
        public Guid PolyclinicId { get; set; }
        public string PolyclinicName { get; set; }
    }
}
