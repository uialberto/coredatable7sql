namespace AppWeb.DTOs.Modules.Core.Usuarios.Request
{
    public enum UsersOrderColumn
    {
        Codigo = 0,
        Nombres = 1,
        Apellidos = 2,
        Email = 3,
        Username = 4,
        FechaCreacion = 5
    }
    public class UsersFilters
    {
        public string TextSearch { get; set; }
        public DateTime? CreateDateUtc { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public UsersOrderColumn OrderBy { get; set; }
        public bool IsAscending { get; set; }
    }
}
