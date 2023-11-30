using Microsoft.EntityFrameworkCore;

namespace Eclipseworks.Persistencia.Contexto
{
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
    }
}
