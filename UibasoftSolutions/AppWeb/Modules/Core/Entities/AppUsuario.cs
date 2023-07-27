using Microsoft.AspNetCore.Identity;

namespace AppWeb.Modules.Core.Entities
{
    public enum UsuarioState
    {
        Creado = 0,
        Autorizado = 1
    }
    public class AppUsuario : IdentityUser
    {
        public AppUsuario()
        {
            CreateDateUtc = DateTime.UtcNow;
            UpdateDateUtc = CreateDateUtc;            
        }
        public long? Codigo { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }

        public UsuarioState Estado { get; set; }
        public DateTime? FechaNac { get; set; }
        public DateTime CreateDateUtc { get; set; }
        public DateTime? UpdateDateUtc { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    }
}
