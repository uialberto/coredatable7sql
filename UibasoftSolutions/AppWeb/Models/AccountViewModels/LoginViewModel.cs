using System.ComponentModel.DataAnnotations;

namespace AppWeb.Models.AccountViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            //Parametro = new ParametroViewModel();
        }
        public string Email { get; set; }
        [Required]
        public string UsernameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
        //public ParametroViewModel Parametro { get; set; }

    }
}
