using System.ComponentModel.DataAnnotations;

namespace Proje_Odevi.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adınızı giriniz.")]
        [StringLength(40, ErrorMessage = "Kullanıcı adı en fazla 40 karakter olabilir.")]
        public string Username { get; set; }

        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifrenizi giriniz.")]
        [MinLength(6, ErrorMessage = "En az 6 karakter girmek zorunludur.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifrenizi tekrar giriniz.")]
        [MinLength(6, ErrorMessage = "En az 6 karakter girmek zorunludur.")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }


    }
}
