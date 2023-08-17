using AppWeb.DTOs.Modules.Core.Usuarios.Request;
using AppWeb.DTOs.Modules.Core.Usuarios.Response;
using AppWeb.Models.AccountViewModels;
using AppWeb.Models.SecurityViewModels;
using AppWeb.Modules.Core.Services;
using AppWeb.ViewModel;
using AutoMapper;
using Fingers10.ExcelExport.ActionResults;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Text.Json;

namespace AppWeb.Controllers.Security
{
    [AllowAnonymous]
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsuarioServices _service;
        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger, IUsuarioServices pService, IMapper pMapper)
        {
            _logger = logger;
            _service = pService;
            _mapper = pMapper;
        }

        public IActionResult Index()
        {
            var idUser = Guid.NewGuid().ToString();
            var model = new ProfileViewModel
            {
                Id = idUser,
                Username = User.Identity.Name,
                Nombres = User.Claims.FirstOrDefault(ele => ele.Type == ClaimTypes.GivenName)?.Value ?? string.Empty,
                Apellidos = User.Claims.FirstOrDefault(ele => ele.Type == ClaimTypes.Surname)?.Value ?? string.Empty,
                Claims = User.Claims,
                ChangePassword = new ChangePasswordViewModel()
                {
                    Id = idUser
                },
                Usuario = new UsuarioViewModel() { }
            };
            return View("Views/Security/Users.cshtml", model.Usuario);
        }
        [HttpPost("search")]
        public async Task<IActionResult> BuscarUsuarios([FromBody] JqueryDataTablesParameters param)
        {
            try
            {

                #region Paginacion y Ordenacion

                var pageIndex = (param.Start / param.Length) + 1;
                var pageSize = param.Length;
                var sortedColumns = param.Order;
                var colum = sortedColumns.FirstOrDefault();
                var orderBy = UsersOrderColumn.Codigo;

                var orderIsAscending = true;
                if (colum != null)
                {
                    var orderByAux = (UsersOrderColumn)colum.Column;
                    orderBy = orderByAux;
                    orderIsAscending = colum.Dir == DTOrderDir.ASC ? true : false;
                }

                #endregion


                var optionSearch = new UsersFilters
                {
                    TextSearch = param.Search?.Value,
                    PageIndex = pageIndex,
                    IsAscending = orderIsAscending,
                    PageSize = pageSize,
                    OrderBy = orderBy,
                };

                HttpContext.Session.SetString(nameof(UsersFilters), JsonSerializer.Serialize(optionSearch));

                var res = await _service.SearchUsersAsync(optionSearch);

                var list = res.Elements;

                var data = list.Select(ele => new
                {
                    Codigo = ele.Codigo,
                    Usuario = ele.UserName,
                    Nombres = ele.Nombres,
                    Apellidos = ele.Apellidos,
                    Email = ele.Email,
                    FechaCreacion = ele.CreateDateUtc,
                    Opciones = string.Empty,
                }).ToList();

                var recordsFiltered = res.TotalFiltered;
                var recordsTotal = res.TotalElements;


                #region 

                //var users = new List<UsuarioDto>()
                //{
                //    new UsuarioDto
                //    {
                //        Id = Guid.NewGuid(),
                //        Username = new Random().Next(10000,90000).ToString(),
                //        Nombres = Guid.NewGuid().ToString().Substring(0,8),
                //        Apellidos = Guid.NewGuid().ToString().Substring(0,10),
                //        Email = $"{Guid.NewGuid().ToString().Substring(0,10)}@abc.com",
                //        FechaNac = DateTime.Now.AddDays(new Random().Next(10,100)),
                //    },
                //    new UsuarioDto
                //    {
                //        Id = Guid.NewGuid(),
                //        Username = new Random().Next(10000,90000).ToString(),
                //        Nombres = Guid.NewGuid().ToString().Substring(0,8),
                //        Apellidos = Guid.NewGuid().ToString().Substring(0,10),
                //        Email = $"{Guid.NewGuid().ToString().Substring(0,10)}@abc.com",
                //        FechaNac = DateTime.Now.AddDays(new Random().Next(10,100)),
                //    },
                //    new UsuarioDto
                //    {
                //        Id = Guid.NewGuid(),
                //        Username = new Random().Next(10000,90000).ToString(),
                //        Nombres = Guid.NewGuid().ToString().Substring(0,8),
                //        Apellidos = Guid.NewGuid().ToString().Substring(0,10),
                //        Email = $"{Guid.NewGuid().ToString().Substring(0,10)}@abc.com",
                //        FechaNac = DateTime.Now.AddDays(new Random().Next(10,100)),
                //    }
                //    ,new UsuarioDto
                //    {
                //        Id = Guid.NewGuid(),
                //        Username = new Random().Next(10000,90000).ToString(),
                //        Nombres = Guid.NewGuid().ToString().Substring(0,8),
                //        Apellidos = Guid.NewGuid().ToString().Substring(0,10),
                //        Email = $"{Guid.NewGuid().ToString().Substring(0,10)}@abc.com",
                //        FechaNac = DateTime.Now.AddDays(new Random().Next(10,100)),
                //    }
                //    ,new UsuarioDto
                //    {
                //        Id = Guid.NewGuid(),
                //        Username = new Random().Next(10000,90000).ToString(),
                //        Nombres = Guid.NewGuid().ToString().Substring(0,8),
                //        Apellidos = Guid.NewGuid().ToString().Substring(0,10),
                //        Email = $"{Guid.NewGuid().ToString().Substring(0,10)}@abc.com",
                //        FechaNac = DateTime.Now.AddDays(new Random().Next(10,100)),
                //    }
                //};

                #endregion

                return new JsonResult(new { draw = param.Draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }
        [HttpGet("getcsv")]
        public async Task<IActionResult> GetCSV()
        {
            try
            {
                var optionSearch = HttpContext.Session.GetString(nameof(UsersFilters));
                var result = await _service.SearchUsersAsync(JsonSerializer.Deserialize<UsersFilters>(optionSearch));
                var list = result.Elements;
                var data = list.Select(ele => new UserExcelResponse
                {
                    Id = ele.Id.ToString(),
                    Codigo = ele.Codigo,
                    Nombres = ele.Nombres,
                    Apellidos = ele.Apellidos,
                    CreateDateUtc = ele.CreateDateUtc.ToString("dd/MM/yyyy") ?? string.Empty,
                });
                var fileName = $"Users_{Guid.NewGuid()}";
                return new CSVResult<UserExcelResponse>(data, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error GetCSV Report. Mensaje: {ex.Message}");
                _logger.LogError(ex.Message);
                return new JsonResult(new { error = "Internal Server Error GetCSV Report" });
            }
           
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(UsuarioViewModel item)
        {
            //var paramDto = _mapper.Map<PersonaDto>(item);
            //var result = await _service.CrearAsync(paramDto);
            //if (result.HasErrors)
            //{
            //    //ToDo Sample
            //    return new JsonResult(new { error = "Internal Server Error" });
            //}
            return NoContent();
        }
        [HttpPost("edit/{id}")]
        public async Task<ActionResult> Edit(long id)
        {
            //var viewModel = new PersonaViewModel();
            //var result = await _service.ObtenerAsync(new PersonaDto() { Id = id });
            //if (result.Element != null)
            //{
            //    viewModel = _mapper.Map<PersonaViewModel>(result.Element);
            //}
            return PartialView("_Edit", new UsuarioViewModel());
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(UsuarioViewModel item)
        {
            //var paramDto = _mapper.Map<PersonaDto>(item);
            //var result = await _service.ActualizarAsync(paramDto);
            //if (result.HasErrors)
            //{
            //    //ToDo Sample
            //    return new JsonResult(new { error = "Internal Server Error" });
            //}

            return NoContent();
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            //var result = await _service.EliminarAsync(new PersonaDto() { Id = id });
            //if (result.HasErrors)
            //{
            //    //ToDo Sample
            //    return new JsonResult(new { error = "Internal Server Error" });
            //}
            return NoContent();
        }

        [HttpGet("getexcel")]
        public async Task<IActionResult> GetExcel()
        {
            try
            {
                var optionSearch = HttpContext.Session.GetString(nameof(UsersFilters));
                var result = await _service.SearchUsersAsync(JsonSerializer.Deserialize<UsersFilters>(optionSearch));
                var list = result.Elements;
                var data = list.Select(ele => new UserExcelResponse
                {
                    Id = ele.Id.ToString(),
                    Codigo = ele.Codigo,
                    Nombres = ele.Nombres,
                    Apellidos = ele.Apellidos,
                    CreateDateUtc = ele.CreateDateUtc.ToString("dd/MM/yyyy") ?? string.Empty,
                });
                var fileName = $"Users_{Guid.NewGuid()}";
                return new ExcelResult<UserExcelResponse>(data, "Usuarios", fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error GetExcel Report. Mensaje: {ex.Message}");
                _logger.LogError(ex.Message);
                return new JsonResult(new { error = "Internal Server Error GetExcel Report" });
            }
        }
    }
}
