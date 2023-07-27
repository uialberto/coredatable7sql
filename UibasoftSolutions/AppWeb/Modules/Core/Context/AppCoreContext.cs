using AppWeb.Helpers.Config;
using AppWeb.Modules.Core.Entities;
using AppWeb.Modules.Core.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace AppWeb.Modules.Core.Context
{
    public partial class AppCoreContext : DbContext
    {
        public AppCoreContext()
        {
        }

        public AppCoreContext(DbContextOptions<AppCoreContext> options) : base(options)
        {
        }

        public virtual DbSet<Parametro> Parametros { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Entity>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ParametroConfig());
        }

    }
}
