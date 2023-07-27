using AppWeb.Models.AccountViewModels;
using Fingers10.ExcelExport.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppWeb.DTOs.Modules.Core.Usuarios.Response
{
    public class UserExcelResponse
    {
        [IncludeInReport(Order = 1)]
        [Display(Name = "Id")]
        public string Id { get; set; }
        [IncludeInReport(Order = 2)]
        [Display(Name = "Codigo")]
        public string Codigo { get; set; }
        [IncludeInReport(Order = 3)]
        [DisplayName("Nombres")]
        public string? Nombres { get; set; }
        [IncludeInReport(Order = 4)]
        [DisplayName("Apellidos")]
        public string? Apellidos { get; set; }
        [NestedIncludeInReport]
        public string UserName { get; set; }
        [NestedIncludeInReport]
        public string Email { get; set; }
        [NestedIncludeInReport]
        public UsuarioState Estado { get; set; }
        [NestedIncludeInReport]
        public DateTime? FechaNac { get; set; }
        [IncludeInReport(Order = 5)]
        [DisplayName("Fecha Creacion")]
        public string CreateDateUtc { get; set; }
        [NestedIncludeInReport]
        public DateTime? UpdateDateUtc { get; set; }
        public bool Active { get; set; }
        public bool EmailConfirmed { get; set; }                
    }
}
