using AppWeb.Models.SecurityViewModels;
using System.Security.Claims;

namespace AppWeb.Models.AccountViewModels
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaCreacionUtc { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UsuarioState Estado { get; set; }
        public DateTime? FechaModUtc { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public ChangePasswordViewModel ChangePassword { get; set; }
        public CreateUserViewModel CreateUser { get; set; }

        public string StatusMessage { get; set; }
    }

    public enum UsuarioState
    {
        Creado = 0,
        Autorizado = 1
    }
}
