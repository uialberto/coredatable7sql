using AppWeb.Models.AccountViewModels;

namespace AppWeb.DTOs.Modules.Core.Usuarios.Response
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Codigo { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UsuarioState Estado { get; set; }
        public DateTime? FechaNac { get; set; }
        public DateTime CreateDateUtc { get; set; }
        public DateTime? UpdateDateUtc { get; set; }
        public bool Active { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<string> IdRoles { get; set; }
    }
}
