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
    public class CreateUserModel
    {
        [Required(ErrorMessage = "Kullanıcı adınızı giriniz.")]
        [StringLength(40, ErrorMessage = "Kullanıcı adı en fazla 40 karakter olabilir.")]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        public string NameSurname { get; set; }

        public bool Locked { get; set; }

        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifrenizi giriniz.")]
        [MinLength(6, ErrorMessage = "En az 6 karakter girmek zorunludur.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifrenizi tekrar giriniz.")]
        [MinLength(6, ErrorMessage = "En az 6 karakter girmek zorunludur.")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }

        public string Role { get; set; } = "User";
    }

}
