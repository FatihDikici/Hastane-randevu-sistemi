using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje_Odevi.Entities
{
    [Table("Kullanıcılar")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }


        public string? NameSurname { get; set; } = null;
        
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        public bool Locked { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
