using AppWeb.Helpers.Config;
using AppWeb.Modules.Core.Entities;
using AppWeb.Modules.Core.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppWeb.Modules.Core.Context
{
    public partial class AppCoreContext : IdentityDbContext<AppUsuario, AppRole, string>
    {
        #region Atributos

        private readonly Guid _instanceId;

        #endregion

        #region Propiedades

        public Guid InstanceId => _instanceId;

        #endregion
        public AppCoreContext()
        {
            _instanceId = Guid.NewGuid();
        }

        public AppCoreContext(DbContextOptions<AppCoreContext> options) : base(options)
        {
            _instanceId = Guid.NewGuid();
        }

        public virtual DbSet<Parametro> Parametros { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Entity>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUsuario>(builder =>
            {
                //b.HasMany(e => e.Claims)
                //    .WithOne()
                //    .HasForeignKey(uc => uc.UserId)
                //    .IsRequired();

                builder.Property(e => e.CreateDateUtc).IsRequired();
                builder.Property(e => e.CreateDateUtc).HasColumnName("CreateDateUtc");
                builder.Property(e => e.CreateDateUtc).HasDefaultValueSql("getutcdate()");

                builder.Property(e => e.Nombres).IsRequired();
                builder.Property(e => e.Nombres).HasColumnName("Nombres");
                builder.Property(e => e.Nombres).IsUnicode(true);
                builder.Property(e => e.Nombres).HasMaxLength(200);

                builder.Property(e => e.Codigo).IsRequired(false);
                builder.Property(e => e.Codigo).HasColumnName("Codigo");

                builder.Property(e => e.Apellidos).HasColumnName("Apellidos");
                builder.Property(e => e.Apellidos).IsUnicode(true);
                builder.Property(e => e.Apellidos).HasMaxLength(200);

                builder.Property(e => e.Estado).IsRequired();
                builder.Property(e => e.Estado).HasColumnName("Estado");
                builder.Property(e => e.Estado).HasConversion<int>();

                builder.Property(e => e.UpdateDateUtc);
                builder.Property(e => e.UpdateDateUtc).HasColumnName("UpdateDateUtc");

                builder.Property(e => e.FechaNac);
                builder.Property(e => e.FechaNac).HasColumnName("FechaNacimiento");

                //// Each User can have many UserLogins
                //b.HasMany(e => e.Logins)
                //    .WithOne()
                //    .HasForeignKey(ul => ul.UserId)
                //    .IsRequired();

                //// Each User can have many UserTokens
                //b.HasMany(e => e.Tokens)
                //    .WithOne()
                //    .HasForeignKey(ut => ut.UserId)
                //    .IsRequired();

                //// Each User can have many entries in the UserRole join table
                builder.HasMany(e => e.UserRoles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.ApplyConfiguration(new ParametroConfig());
        }

    }
}
