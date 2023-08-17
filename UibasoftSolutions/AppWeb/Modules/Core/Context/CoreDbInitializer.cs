using AppWeb.Helpers.Options;
using AppWeb.Modules.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace AppWeb.Modules.Core.Context
{
    public class CoreDbInitializer
    {
        public static void Initialize(AppCoreContext context, EnvSettingOptions? envSetting, ApiWebSettingOptions? appSetting,
            UserManager<AppUsuario> userManager, RoleManager<AppRole> roleManager)
        {
            context.Database.EnsureCreated();

            SeedConfigurations(context, appSetting);

            SeedRoles(roleManager);
            SeedUsers(userManager);
            SeedUsersAll(userManager, context);
            SeedRolesAll(roleManager, context);

        }
        public static void SeedUsers(UserManager<AppUsuario> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                AppUsuario user = new AppUsuario()
                {
                    UserName = "admin",
                    Email = "admin@farmacorp.com",
                    Nombres = "Alberto",
                    Apellidos = "Baigorria",
                    Codigo = 1
                };
                IdentityResult result = userManager.CreateAsync(user, "Abc123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "admin").Wait();
                    userManager.AddToRoleAsync(user, "public").Wait();
                }
            }

            if (userManager.FindByNameAsync("lbaigorria").Result == null)
            {
                AppUsuario user = new AppUsuario()
                {
                    UserName = "lbaigorria",
                    Email = "lbaigorria@farmacorp.com",
                    Nombres = "Alberto",
                    Apellidos = "Express",
                    Codigo = 2
                };
                IdentityResult result = userManager.CreateAsync(user, "Abc123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "admin-oneexpress").Wait();
                    userManager.AddToRoleAsync(user, "public").Wait();
                }
            }
        }

        public static void SeedRolesAll(RoleManager<AppRole> manager, AppCoreContext context)
        {
            if (context.Roles.Count() >= 20)
            {
                return;
            }
            for (int i = 1; i <= 30; i++)
            {
                var idRole = Guid.NewGuid().ToString().Substring(0, 20);
                var role = new AppRole
                {
                    Id = (100 + i).ToString(),
                    Name = (100 + i).ToString(),
                    Active = true,
                    Descripcion = Guid.NewGuid().ToString().Substring(0, 15),
                    CreateDateUtc = DateTime.UtcNow.AddDays(-i),

                };
                var result = manager.CreateAsync(role).Result;
            }
        }

        public static void SeedUsersAll(UserManager<AppUsuario> userManager, AppCoreContext context)
        {
            if (context.Users.Count() >= 10)
            {
                return;
            }
            for (int i = 3; i <= 30; i++)
            {
                AppUsuario user = new AppUsuario()
                {
                    Nombres = Guid.NewGuid().ToString().Substring(0, 20),
                    Apellidos = Guid.NewGuid().ToString().Substring(0, 15),
                    UserName = (new Random().Next(1000, 15000)).ToString(),
                    Email = $"{Guid.NewGuid().ToString().Substring(0, 10)}@abc.com",
                    Estado = UsuarioState.Creado,
                    FechaNac = new DateTime(new Random().Next(1900, 2020), 01, 01),
                    CreateDateUtc = new DateTime(new Random().Next(2022, 2023), 01, 01),
                    Codigo = i
                };
                IdentityResult result = userManager.CreateAsync(user, "Abc123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "admin-oneexpress").Wait();
                    userManager.AddToRoleAsync(user, "public").Wait();
                }

            }
        }
        public static void SeedRoles(RoleManager<AppRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("public").Result)
            {
                var role = new AppRole
                {
                    Id = "public",
                    Name = "Public",
                    Active = true,
                    Descripcion = "Publico",

                };
                var roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                var role = new AppRole
                {
                    Id = "admin",
                    Name = "Admin",
                    Active = true,
                    Descripcion = "Administrador",

                };
                var roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("admin-oneexpress").Result)
            {
                var role = new AppRole
                {
                    Id = "admin-oneexpress",
                    Name = "Admin-OneExpress",
                    Active = true,
                    Descripcion = "Administrador Aplicacion"

                };
                var roleResult = roleManager.CreateAsync(role).Result;
            }
        }
        public static void SeedConfigurations(AppCoreContext context, ApiWebSettingOptions? appSetting)
        {
            if (context.Parametros.Any())
            {
                return;
            }
            var hoyUtc = DateTime.UtcNow;
            var parametro = new Parametro()
            {
                Empresa = appSetting.Empresa,
                Nit = appSetting.Nit,
                DefaultPageIndex = appSetting.DefaultPageIndex,
                DefaultPageSize = appSetting.DefaultPageSize,
                CreateDateUtc = hoyUtc
            };
            context.Parametros.Add(parametro);
            context.SaveChanges();

        }
    }
}
