using System.Diagnostics;
using System.Reflection;
using Eclipseworks.Dominio.Notifier;
using Eclipseworks.Dominio.Notifier.Interfaces;
using Eclipseworks.Persistencia.Contexto;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Eclipseworks.Api;
public static class Bootstrapper
{
    public static IServiceCollection AddInfraApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddDbContext<EclipseworksContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Eclipseworks"), options =>
                    {
                        options.MigrationsAssembly(typeof(EclipseworksContext).Assembly.FullName);
                        options.EnableRetryOnFailure();
                    }
                ).LogTo(message => Debug.WriteLine(message))
                .EnableDetailedErrors(true)
                .EnableSensitiveDataLogging(true);
        });

        services.AddScoped<Notifier>();

        services.AddScoped<INotifier>(provider => provider.GetRequiredService<Notifier>());
        services.AddAutoMapper(Assembly.GetEntryAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetEntryAssembly());
        return services;
    }
}
