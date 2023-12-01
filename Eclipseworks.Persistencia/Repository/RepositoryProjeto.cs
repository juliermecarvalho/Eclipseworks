using Eclipseworks.Dominio.Core;
using Eclipseworks.Dominio.IRepository;
using Eclipseworks.Dominio.IRepository.Base;
using Eclipseworks.Dominio.Notifier.Interfaces;
using Eclipseworks.Persistencia.Repository.Base;

namespace Eclipseworks.Persistencia.Repository;

public class RepositoryProjeto : Repository<Projeto>, IRepositoryProjeto
{
    public RepositoryProjeto(IUnitofWork unitofWork, INotifier notifier) : base(unitofWork, notifier)
    {
    }
}