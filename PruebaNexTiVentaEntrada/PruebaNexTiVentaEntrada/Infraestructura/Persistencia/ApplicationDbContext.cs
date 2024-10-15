using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaNexTiVentaEntrada.Modelos;
//using Examen.Core.Domain.Entity;

namespace Examen.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Evento>().HasQueryFilter(e => !e.Desabilitado);
        }
    }
}
