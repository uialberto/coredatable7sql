using AppWeb.DTOs.Modules.Core.Usuarios.Request;
using AppWeb.DTOs.Modules.Core.Usuarios.Response;
using AppWeb.Helpers.Config;
using AppWeb.Helpers.Options;
using AppWeb.Modules.Core.Context;
using AppWeb.Modules.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AppWeb.Modules.Core.Services.Impl
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly ILogger<UsuarioServices> _logger;
        private readonly ApiWebSettingOptions _appSetting;
        private readonly AppCoreContext _dbContext;
        private readonly UserManager<AppUsuario> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public UsuarioServices(ILogger<UsuarioServices> pLogger, IOptions<ApiWebSettingOptions> pSettings, AppCoreContext pContext,
            UserManager<AppUsuario> pUserManager, RoleManager<AppRole> pRoleManager)
        {
            _appSetting = pSettings?.Value ?? throw new ArgumentNullException(nameof(pSettings));
            _logger = pLogger ?? throw new ArgumentNullException(nameof(pLogger));
            _dbContext = pContext ?? throw new ArgumentNullException(nameof(pContext));
            _userManager = pUserManager ?? throw new ArgumentNullException(nameof(pUserManager));
            _roleManager = pRoleManager ?? throw new ArgumentNullException(nameof(pRoleManager));
        }
        public async Task<SearchUsersResponse> SearchUsersAsync(UsersFilters filters)
        {
            filters.PageIndex = filters.PageIndex == 0 ? _appSetting.DefaultPageIndex : filters.PageIndex;
            filters.PageSize = filters.PageSize == 0 ? _appSetting.DefaultPageSize : filters.PageSize;

            var query = _dbContext.Users.AsQueryable();

            #region Filtros

            if (!string.IsNullOrWhiteSpace(filters.TextSearch))
            {

                query = query.Where(ele => ele.Nombres.ToLower().Contains(filters.TextSearch.ToLower()) ||
                                              ele.Apellidos.ToLower().Contains(filters.TextSearch.ToLower()) ||
                                              ele.Email.ToLower().Contains(filters.TextSearch.ToLower()) ||
                                              ele.Codigo.ToString().ToLower().Contains(filters.TextSearch.ToLower()) ||
                                              ele.UserName.ToLower().Contains(filters.TextSearch.ToLower()));

            }

            if (filters.CreateDateUtc.HasValue)
            {
                query = query.Where(ele => ele.CreateDateUtc.ToShortDateString() == filters.CreateDateUtc.Value.ToShortDateString());
            }

            #endregion

            #region Ordenacion

            switch (filters.OrderBy)
            {
                case UsersOrderColumn.Codigo:
                    {
                        query = filters.IsAscending ? query.OrderBy(ele => ele.Codigo).ThenBy(ele => ele.Codigo)
                            : query.OrderByDescending(ele => ele.Codigo).ThenBy(ele => ele.Codigo);
                    }
                    break;
                case UsersOrderColumn.Nombres:
                    {
                        query = filters.IsAscending ? query.OrderBy(ele => ele.Nombres).ThenBy(ele => ele.Nombres)
                           : query.OrderByDescending(ele => ele.Nombres).ThenBy(ele => ele.Nombres);
                    }
                    break;
                case UsersOrderColumn.Email:
                    {
                        query = filters.IsAscending ? query.OrderBy(ele => ele.Email).ThenBy(ele => ele.Email)
                          : query.OrderByDescending(ele => ele.Email).ThenBy(ele => ele.Email);
                    }
                    break;
                default:
                    {
                        query = filters.IsAscending ? query.OrderBy(ele => ele.Id).ThenBy(ele => ele.Id)
                         : query.OrderByDescending(ele => ele.Id).ThenBy(ele => ele.Id);
                    }

                    break;
            }


            #endregion

            #region Paginacion

            var resultList = PageList<AppUsuario>.Create(query, filters.PageIndex, filters.PageSize);

            resultList.TotalElements = await _dbContext.Users.CountAsync();

            #endregion

            #region Resultado 

            var result = new SearchUsersResponse
            {
                TotalPage = resultList.TotalPages,
                TotalElements = resultList.TotalElements,
                PageIndex = resultList.PageIndex,
                PageSize = resultList.PageSize,
                TotalFiltered = resultList.TotalFiltered,
                Elements = resultList.ToList().Select(ele => new UserResponse
                {
                    Id = ele.Id,
                    Codigo = ele.Codigo.HasValue ? ele.Codigo.ToString() : string.Empty,
                    Nombres = ele.Nombres ?? string.Empty,
                    Apellidos = ele.Apellidos ?? string.Empty,
                    Email = ele.Email,
                    UserName = ele.UserName,
                    CreateDateUtc = ele.CreateDateUtc,
                    EmailConfirmed = ele.EmailConfirmed,
                    IdRoles = ele.UserRoles?.Select(ele => ele.RoleId).ToList(),
                    Active = ele.Active,
                }).ToList()
            };

            #endregion

            return result;
        }
    }
}
