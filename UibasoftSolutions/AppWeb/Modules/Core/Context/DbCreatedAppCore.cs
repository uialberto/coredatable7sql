using AppWeb.Helpers.Options;
using AppWeb.Modules.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AppWeb.Modules.Core.Context
{
    public static class DbCreatedAppCore
    {
        public static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppCoreContext>();
                    var apiWebSetting = services.GetRequiredService<IOptions<ApiWebSettingOptions>>()?.Value;
                    var envSettings = services.GetRequiredService<IOptions<EnvSettingOptions>>()?.Value;
                    var userManager = services.GetRequiredService<UserManager<AppUsuario>>();
                    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

                    CoreDbInitializer.Initialize(context, envSettings, apiWebSetting, userManager, roleManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, $"Ocurrió un error inicializando la BD: {nameof(DbCreatedAppCore)}");
                }
            }
        }
    }
}
