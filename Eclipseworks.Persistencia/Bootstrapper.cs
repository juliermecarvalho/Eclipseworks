using Eclipseworks.Dominio.IRepository.Base;
using Eclipseworks.Persistencia.Contexto;
using Eclipseworks.Persistencia.Repository.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Eclipseworks.Persistencia;

public static class Bootstrapper
{
    public static IServiceCollection AddInfraPersistencia(this IServiceCollection services)
    {
        services.AddScoped<EclipseworksContext>();
        services.AddScoped<IUnitofWork, UnitofWork>();
        var interfaceRepository = typeof(IRepository<>).Assembly.GetTypes()
            .Where(t => t.Name.StartsWith("IRepository") && t.IsInterface);
        var classesRepository = typeof(Repository<>).Assembly.GetTypes().Where(t => t.Name.StartsWith("Repository") && t.IsClass);

        foreach (var classRepository in classesRepository)
        {
            var ir = interfaceRepository.FirstOrDefault(t => t.IsInterface && t.Name == $"I{classRepository.Name}");

            if (!classRepository.IsAbstract && ir != null)
            {
                services.AddScoped(ir, classRepository);
            }
        }

        return services;
    }
}
