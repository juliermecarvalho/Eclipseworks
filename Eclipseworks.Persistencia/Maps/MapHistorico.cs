using Eclipseworks.Dominio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eclipseworks.Persistencia.Maps;

public class MapHistorico : IEntityTypeConfiguration<Historico>
{
    public void Configure(EntityTypeBuilder<Historico> builder)
    {
        builder.ToTable("historicos");
           
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Data).HasColumnName("data").IsRequired();
        
        builder.Property(x => x.Operacao ).HasColumnName("operacao").HasMaxLength(250).IsRequired();
        builder.Property(x => x.Campo ).HasColumnName("campo").HasMaxLength(250).IsRequired();
        builder.Property(x => x.ValorAntigo ).HasColumnName("valor_antigo").HasMaxLength(250).IsRequired(false);
        builder.Property(x => x.NovoValor ).HasColumnName("novo_valor").HasMaxLength(250).IsRequired(false);

        builder.Property(x => x.UsuarioId).HasColumnName("usuario_id").IsRequired();
        builder.HasOne(x => x.Usuario).WithMany(x => x.Historicos).HasForeignKey(x => x.UsuarioId).OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(x => x.TarefaId).HasColumnName("tarefa_id").IsRequired();
        builder.HasOne(x => x.Tarefa).WithMany(x => x.Historicos).HasForeignKey(x => x.TarefaId);



    }
}