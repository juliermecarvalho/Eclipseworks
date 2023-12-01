using Eclipseworks.Dominio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eclipseworks.Persistencia.Maps;

public class MapProjeto : IEntityTypeConfiguration<Projeto>
{
    public void Configure(EntityTypeBuilder<Projeto> builder)
    {
        builder.ToTable("projetos");
           
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(250).IsRequired();

        builder.Property(x => x.CriadorDoProjetoId).HasColumnName("criador_do_projeto_id").IsRequired();
        builder.HasOne(x => x.CriadorDoProjeto).WithMany(x => x.Projetos).HasForeignKey(x => x.CriadorDoProjetoId);

        builder.HasMany(x => x.Tarefas).WithOne(x => x.Projeto).HasForeignKey(x => x.ProjetoId);


    }
}