using Eclipseworks.Dominio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eclipseworks.Persistencia.Maps;

public class MapUsuario : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");
           
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(250).IsRequired();

        builder.HasMany(x => x.Projetos).WithOne(x => x.CriadorDoProjeto).HasForeignKey(x => x.CriadorDoProjetoId);
        builder.HasMany(x => x.Historicos).WithOne(x => x.Usuario).HasForeignKey(x => x.UsuarioId);
    }
}