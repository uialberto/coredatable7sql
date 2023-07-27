using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AppWeb.Modules.Core.Entities
{
    public class AppRole : IdentityRole
    {
        public AppRole()
        {
            CreateDateUtc = DateTime.UtcNow;
            UpdateDateUtc = CreateDateUtc;
        }
        [StringLength(80)]
        public string? Descripcion { get; set; }

        public DateTime CreateDateUtc { get; set; }
        public DateTime? UpdateDateUtc { get; set; }
        public bool Active { get; set; }
    }
}
