using Eclipseworks.Dominio.Core;
using Eclipseworks.Dominio.IRepository;
using Eclipseworks.Dominio.IRepository.Base;
using Eclipseworks.Dominio.Notifier.Interfaces;
using Eclipseworks.Persistencia.Repository.Base;

namespace Eclipseworks.Persistencia.Repository
{
    public class RepositoryUsuario : Repository<Usuario>, IRepositoryUsuario
    {
     
        public RepositoryUsuario(IUnitofWork unitofWork, INotifier notifier) 
            : base(unitofWork, notifier)
        {
        }
    }
}
