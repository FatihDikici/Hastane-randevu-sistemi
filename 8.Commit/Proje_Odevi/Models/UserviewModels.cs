using System.ComponentModel.DataAnnotations;

namespace Proje_Odevi.Models
{
    public class UserModel
    {
       
        public Guid Id { get; set; }

        public string? NameSurname { get; set; } = null;
       
        public string Username { get; set; }
        public bool Locked { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
     
        public string ProfileImageFileName { get; set; } = "noimage.jpg";

        public string Role { get; set; } = "User";
    }
}
