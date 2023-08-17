using AppWeb.DTOs.Modules.Core.Usuarios.Request;
using AppWeb.DTOs.Modules.Core.Usuarios.Response;

namespace AppWeb.Modules.Core.Services
{
    public interface IUsuarioServices
    {
        Task<SearchUsersResponse> SearchUsersAsync(UsersFilters filters);
    }
}
