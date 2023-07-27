using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppWeb.Models.SecurityViewModels
{
    public class CreateUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Nombres")]
        [Required]
        public string Nombres { get; set; }

        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [Display(Name = "UserName")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirma Contraseña")]
        [Required]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }


        public string ReturnUrl { get; set; }
    }
}
