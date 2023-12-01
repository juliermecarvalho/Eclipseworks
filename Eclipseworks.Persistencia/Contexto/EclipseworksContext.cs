using Eclipseworks.Dominio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Eclipseworks.Persistencia.Contexto;

public class EclipseworksContext : DbContext
{
    
    public EclipseworksContext(DbContextOptions<EclipseworksContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EclipseworksContext).Assembly);
        base.OnModelCreating(modelBuilder);

    }

    public async Task<int> SaveChangesAsync(long usuarioId)
    {
        if (usuarioId > 0)
        {
            List<Historico> logs = SaveChangesLog(usuarioId);
            await base.SaveChangesAsync();

            SetarId(logs);
            foreach (Historico log in logs)
            {
                this.Add(log);
            }
        }

        return await base.SaveChangesAsync(new CancellationToken());
    }
    
    public List<Historico> SaveChangesLog(long usuarioId)
    {
        var logs = new List<Historico>();

        foreach (var entry in GetChangedEntries())
        {
            var tableName = entry.Metadata.GetTableName();
            if (IsTableMonitored(tableName))
            {
                var primaryKeyInfo = GetPrimaryKeyInfo(entry);

                foreach (var property in entry.Properties)
                {
                    var fieldName = property.Metadata.Name;
                    var oldValue = GetOldValue(entry, property);
                    var newValue = GetNewValue(entry, property);

                    if (ShouldLog(entry, property, oldValue, newValue))
                    {
                        var logEntry = CreateHistorico(usuarioId, primaryKeyInfo, entry.State.ToString(), fieldName, oldValue, newValue);
                        logs.Add(logEntry);
                    }
                }
            }
        }

        return logs;
    }

    private IEnumerable<EntityEntry> GetChangedEntries()
    {
        return ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);
    }

    private string  GetPrimaryKeyInfo(EntityEntry entry)
    {
        var primaryKey = entry.Metadata.FindPrimaryKey();
        var primaryKeyName = primaryKey.Properties.First().Name;
        var primaryKeyValue = entry.Property(primaryKeyName).CurrentValue.ToString();

        if (entry.Metadata.GetTableName() == "comentarios")
        {
            primaryKeyValue = entry.Property("TarefaId").CurrentValue.ToString();
        }

        return  primaryKeyValue;
    }

    private bool IsTableMonitored(string tableName)
    {
        return tableName == "tarefas" || tableName == "comentarios";
    }

    private string GetOldValue(EntityEntry entry, PropertyEntry property)
    {
        return entry.State == EntityState.Added ? null : OldValue(entry, entry.GetDatabaseValues(), property.Metadata.Name);
    }

    private string GetNewValue(EntityEntry entry, PropertyEntry property)
    {
        return NewValue(entry, property);
    }

    private bool ShouldLog(EntityEntry entry, PropertyEntry property, string oldValue, string newValue)
    {
        return entry.State == EntityState.Added || entry.State == EntityState.Deleted || (property.IsModified && oldValue != newValue);
    }

    private Historico CreateHistorico(long usuarioId, string tarefaId, string operacao, string campo, string? valorAntigo, string? novoValor)
    {
        return new Historico
        {
            UsuarioId = usuarioId,
            TarefaId = Convert.ToInt64(tarefaId),
            Operacao = operacao,
            Campo = campo,
            ValorAntigo = valorAntigo,
            NovoValor = novoValor
        };
    }
    
    private void SetarId(List<Historico> logs)
    {
        var logAdicionado = logs.FirstOrDefault(l => l.Operacao == "Added");

        if (logAdicionado != null)
        {
            var entityEntry = ChangeTracker.Entries().FirstOrDefault();
            long id = entityEntry?.Property("Id").CurrentValue as long? ?? 0;

            foreach (var log in logs.Where(l => l.TarefaId < 1))
            {
                log.TarefaId = id;
            }

            var idLog = logs.FirstOrDefault(l => l.Campo == "Id");
            if (idLog != null)
            {
                idLog.NovoValor = id.ToString();
            }
        }
    }

    private string? NewValue(EntityEntry entry, PropertyEntry property)
    {
        try
        {
            return entry.State == EntityState.Deleted ? null : property.CurrentValue?.ToString();
        }
        catch (Exception)
        {

            return null;
        }
    }
    private string? OldValue(EntityEntry entry, PropertyValues? databaseValues, string fieldName)
    {
        try
        {
            if (entry.State == EntityState.Added)
            {
                return null;
            }

            return databaseValues?.GetValue<object>(fieldName) == null ? null : databaseValues?.GetValue<object>(fieldName).ToString();
        }
        catch (Exception)
        {

            return null;
        }
    }
}