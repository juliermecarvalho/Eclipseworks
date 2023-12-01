using Microsoft.EntityFrameworkCore;

namespace Eclipseworks.Dominio.IRepository.Base;

public interface IUnitofWork : IDisposable
{
    DbContext Context { get; }

    Task Commit(long usuarioId);
}