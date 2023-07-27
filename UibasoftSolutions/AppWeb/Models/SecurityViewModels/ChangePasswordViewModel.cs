using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppWeb.Models.SecurityViewModels
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Contraseña Actual")]
        [DataType(DataType.Password)]
        public string PasswordNow { get; set; }

        [Display(Name = "Nueva Contraseña")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmar Nueva Contraseña")]
        [Required]
        [DataType(DataType.Password)]
        public string RePassword { get; set; }
        public string ReturnUrl { get; set; }
    }
}
