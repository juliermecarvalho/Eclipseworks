using System.Linq.Expressions;
using Eclipseworks.Dominio.Core.Base;

namespace Eclipseworks.Dominio.IRepository.Base;

public interface IRepository<TEntidade> : IDisposable where TEntidade : Entity
{

    Task SaveAsync(TEntidade entidade);
    Task DeleteAsync(long id);
    void Delete(TEntidade entidade);

    public Task CommitAsync(long usuarioId = 0);

    Task<TEntidade?> GetAsync(long id, bool asNoTracking = true, params Expression<Func<TEntidade, object>>[] includes);

    Task<IEnumerable<TEntidade>> ListAsync(
        Expression<Func<TEntidade, bool>>? filter = null,
        Func<IQueryable<TEntidade>, IOrderedQueryable<TEntidade>>? orderBy = null,
        bool asNoTracking = true,
        params Expression<Func<TEntidade, object>>[] includes);



}