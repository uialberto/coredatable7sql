using AppWeb.Helpers.Options;
using AppWeb.Modules.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace AppWeb.Helpers.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var inMemory = configuration["CONFIG_COREDATABLE_INMEMORY"] ?? string.Empty;
            if (inMemory == "true")
            {
                services.AddDbContext<AppCoreContext>(options =>
                {
                    options.UseInMemoryDatabase("webappcoredatable");
                });
            }
            else
            {
                var cnxCore = configuration["CONFIG_CORE7DATABLE_CORE_CN"] ?? string.Empty;
                services.AddDbContext<AppCoreContext>(options =>
                   options.UseSqlServer(cnxCore/*migra => migra.MigrationsAssembly("ApiWeb")*/));
            }                    
            return services;
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiSecurityModuleOption>(options => configuration.GetSection("ApiSecurityModule").Bind(options));
            services.Configure<EnvSettingOptions>(options => configuration.GetSection("EnvSettingsConfig").Bind(options));
            services.Configure<ApiWebSettingOptions>(options => configuration.GetSection("ApiWebSettingsOptions").Bind(options));
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IServiceToken, ServiceToken>(provider =>
            //{
            //    var logger = provider.GetRequiredService<ILogger<ServiceToken>>();
            //    var configuration = provider.GetRequiredService<IConfiguration>();
            //    var secret = configuration["CONFIG_NCORE7SQL_SECURITY_AUTHJWT_SECRET"] ?? string.Empty;
            //    int.TryParse(configuration["CONFIG_NCORE7SQL_SECURITY_AUTHJWT_EXPIREMIN"], out var expireMin);
            //    return new ServiceToken(logger, new AuthJwtOptions()
            //    {
            //        Secret = secret,
            //        ExpiresMin = expireMin
            //    });
            //});
            //services.AddScoped<IServiceAccount, ServiceAccount>();
            return services;
        }

        public static IServiceCollection AddSwaggers(this IServiceCollection services, string xmlFileName)
        {
            //services.AddSwaggerGen(doc =>
            //{
            //    doc.SwaggerDoc("v1", new OpenApiInfo { Title = "API Documentacion", Version = "v1" });
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
            //    doc.IncludeXmlComments(xmlPath);

            //    #region Basic

            //    //doc.AddSecurityDefinition("basic", new OpenApiSecurityScheme
            //    //{
            //    //    Name = "Authorization",
            //    //    Type = SecuritySchemeType.Http,
            //    //    Scheme = "basic",
            //    //    In = ParameterLocation.Header,
            //    //    Description = "Basic Authorization Header Using the Bearer Scheme."
            //    //});
            //    //doc.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    //{
            //    //    {
            //    //          new OpenApiSecurityScheme
            //    //            {
            //    //                Reference = new OpenApiReference
            //    //                {
            //    //                    Type = ReferenceType.SecurityScheme,
            //    //                    Id = "basic"
            //    //                }
            //    //            },
            //    //            new string[] {}
            //    //    }
            //    //});

            //    #endregion

            //    #region Bearer

            //    doc.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        In = ParameterLocation.Header,
            //        Description = "Por favor inserte el Token Bearer JWT.",
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.ApiKey
            //    });

            //    doc.AddSecurityRequirement(new OpenApiSecurityRequirement {
            //       {
            //         new OpenApiSecurityScheme
            //         {
            //           Reference = new OpenApiReference
            //           {
            //             Type = ReferenceType.SecurityScheme,
            //             Id = "Bearer"
            //           }
            //          },
            //          new string[] { }
            //        }
            //      });

            //    #endregion

            //});

            return services;
        }

        public static IServiceCollection AddHangFire(this IServiceCollection services, IConfiguration configuration)
        {
            //var appSetting = services.BuildServiceProvider().GetRequiredService<IOptions<AppSettingsConfigOptions>>().Value;


            //services.AddHangfire(config => config
            //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseRecommendedSerializerSettings()
            //    .UseSqlServerStorage(configuration.GetConnectionString("AppJobContext"), new SqlServerStorageOptions
            //    {
            //        CommandBatchMaxTimeout = TimeSpan.FromMinutes(8),
            //        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //        QueuePollInterval = TimeSpan.FromMinutes(2),
            //        UseRecommendedIsolationLevel = true,
            //        UsePageLocksOnDequeue = true,
            //        DisableGlobalLocks = true,
            //    }));

            //services.AddHangfireServer();

            return services;
        }


    }
}
