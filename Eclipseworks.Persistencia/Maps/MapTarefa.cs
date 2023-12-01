using Eclipseworks.Dominio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eclipseworks.Persistencia.Maps;

public class MapTarefa : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.ToTable("tarefas");
           
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Titulo).HasColumnName("titulo").HasMaxLength(250).IsRequired();
        builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(250).IsRequired();
        builder.Property(x => x.DataVencimento).HasColumnName("data_vencimento").IsRequired();
        builder.Property(x => x.Status ).HasColumnName("status ").IsRequired();

        builder.Property(x => x.ProjetoId).HasColumnName("projeto_id").IsRequired();
        builder.HasOne(x => x.Projeto).WithMany(x => x.Tarefas).HasForeignKey(x => x.ProjetoId);

        builder.HasMany(x => x.Comentarios).WithOne(x => x.Tarefa).HasForeignKey(x => x.TarefaId).OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Historicos).WithOne(x => x.Tarefa).HasForeignKey(x => x.TarefaId).OnDelete(DeleteBehavior.Cascade);


    }
}