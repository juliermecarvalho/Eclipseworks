using Eclipseworks.Dominio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eclipseworks.Persistencia.Maps;

public class MapComentario : IEntityTypeConfiguration<Comentario>
{
    public void Configure(EntityTypeBuilder<Comentario> builder)
    {
        builder.ToTable("comentarios");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Texto).HasColumnName("texto").IsRequired();
        

        builder.Property(x => x.TarefaId).HasColumnName("tarefa_id").IsRequired();
        builder.HasOne(x => x.Tarefa).WithMany(x => x.Comentarios).HasForeignKey(x => x.TarefaId);

    }
}