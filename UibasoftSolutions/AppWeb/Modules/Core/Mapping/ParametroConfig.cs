using AppWeb.Modules.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AppWeb.Modules.Core.Mapping
{
    public class ParametroConfig : IEntityTypeConfiguration<Parametro>
    {
        public void Configure(EntityTypeBuilder<Parametro> builder)
        {
            builder.Property(e => e.Empresa).HasColumnName("Empresa");
            builder.Property(e => e.Empresa).HasMaxLength(150);
            builder.Property(e => e.Empresa).IsUnicode(false);
            builder.Property(e => e.Empresa).IsRequired(false);

            builder.Property(e => e.Nit).HasColumnName("Nit");
            builder.Property(e => e.Nit).HasMaxLength(20);
            builder.Property(e => e.Nit).IsUnicode(false);
            builder.Property(e => e.Nit).IsRequired(false);

            builder.Property(e => e.CreateDateUtc).IsRequired();
            builder.Property(e => e.CreateDateUtc).HasColumnName("CreateDateUtc");

            builder.Property(e => e.DefaultPageIndex).IsRequired(false);
            builder.Property(e => e.DefaultPageIndex).HasColumnName("DefaultPageIndex");

            builder.Property(e => e.DefaultPageSize).IsRequired(false);
            builder.Property(e => e.DefaultPageSize).HasColumnName("DefaultPageSize");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.Id).HasColumnName("IdParametro");
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.ToTable("Parametros", "core");

        }
    }
}
