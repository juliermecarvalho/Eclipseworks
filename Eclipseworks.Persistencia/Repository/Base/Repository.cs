using System.Linq.Expressions;
using Eclipseworks.Dominio.Core.Base;
using Eclipseworks.Dominio.IRepository.Base;
using Eclipseworks.Dominio.Notifier.Interfaces;
using Eclipseworks.Persistencia.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eclipseworks.Persistencia.Repository.Base;

public abstract class Repository<TEntidade> : IRepository<TEntidade> where TEntidade : Entity
{
    protected readonly IUnitofWork UnitofWork;
    private readonly INotifier _notifier;

    protected Repository(IUnitofWork unitofWork, INotifier notifier)
    {
        UnitofWork = unitofWork;
        _notifier = notifier;
    }

    public async Task SaveAsync(TEntidade entidade)
    {
        if (!_notifier.HasNotification)
        {
           
            if (entidade.Id == 0)
            {
                await UnitofWork.Context.AddAsync(entidade);
            }
            else
            {
                UnitofWork.Context.Update(entidade);
            }
        }

    }
    
    public async Task DeleteAsync(long id)
    {
        if (!_notifier.HasNotification)
        {
            var obj = await GetAsync(id, asNoTracking: false);
            if (obj is { Id: > 0 })
            {

                _ = UnitofWork.Context.Remove(obj);
            }
            else
            {
                _notifier.Add(new Exception("Erro ao excluir registro. Registro não pode ser encontrado"));
            }
        }
    }

    public void Delete(TEntidade entidade)
    {
          UnitofWork.Context.Remove(entidade);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    public virtual async Task<TEntidade?> GetAsync(long id, bool asNoTracking = true, params Expression<Func<TEntidade, object>>[] includes)
    {
        var query = includes.Aggregate(
            UnitofWork.Context.Set<TEntidade>().AsQueryable(),
            (current, include) => current.Include(include.AsPath()));
        if(asNoTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(obj => obj.Id == id);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    public virtual async Task<IEnumerable<TEntidade>> ListAsync(
        Expression<Func<TEntidade, bool>>? filter = null,
        Func<IQueryable<TEntidade>, IOrderedQueryable<TEntidade>>? orderBy = null,
        bool asNoTracking = true,
        params Expression<Func<TEntidade, object>>[] includes)
    {
        var query = UnitofWork.Context.Set<TEntidade>().AsQueryable();
        if (filter is not null)
        {
            query = query.Where(filter);
        }

        query = orderBy is not null ? orderBy(query) : query.OrderBy(q => q.Id);

        query = includes
            .Aggregate(
                query,
                (current, include) => current.Include(include.AsPath())
            );
        if (asNoTracking)
            query = query.AsNoTracking();
        return await query.ToListAsync();
    }

    
    public async Task CommitAsync(long usuarioId)
    {
        await UnitofWork.Commit(usuarioId);
    }

    public void Dispose()
    {
        UnitofWork.Context?.Dispose();
    }
  
}