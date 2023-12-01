using Eclipseworks.Dominio.Core;
using Eclipseworks.Dominio.IRepository;
using Eclipseworks.Dominio.IRepository.Base;
using Eclipseworks.Dominio.Notifier.Interfaces;
using Eclipseworks.Persistencia.Repository.Base;

namespace Eclipseworks.Persistencia.Repository;

public class RepositoryTarefa : Repository<Tarefa>, IRepositoryTarefa
{
    public RepositoryTarefa(IUnitofWork unitofWork, INotifier notifier) : base(unitofWork, notifier)
    {
    }
}