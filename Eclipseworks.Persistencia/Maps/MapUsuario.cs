using Eclipseworks.Dominio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eclipseworks.Persistencia.Maps
{
    public class MapUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");
           
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(250).IsRequired();


        }
    }
}
