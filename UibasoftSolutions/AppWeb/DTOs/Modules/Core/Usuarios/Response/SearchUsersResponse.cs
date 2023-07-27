namespace AppWeb.DTOs.Modules.Core.Usuarios.Response
{
    public class SearchUsersResponse
    {
        public List<UserResponse> Elements { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalFiltered { get; set; }
        public int TotalElements { get; set; }
    }
}
