using PruebaNexTiVentaEntrada.Repositorios.Contrato;
using Examen.Infrastructure.Persistence;
using PruebaNexTiVentaEntrada.Modelos;
using Microsoft.EntityFrameworkCore;

namespace PruebaNexTiVentaEntrada.Repositorios.Implemetacion
{
    public class EventoRepository : IEventoRepository
    {
        private readonly ApplicationDbContext _context;

        public EventoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Evento>> GetAllEventos()
        {
            return await _context.Eventos.ToListAsync();
        }

        public async Task<Evento> GetEventoById(int id)
        {
            return await _context.Eventos.FindAsync(id);
        }

        public async Task AddEvento(Evento nuevoEvento)
        {
            _context.Eventos.Add(nuevoEvento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEvento(Evento eventToUpdate)
        {
            _context.Eventos.Update(eventToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEvento(int id)
        {
            var eventoItem = await _context.Eventos.FindAsync(id);
            if (eventoItem != null)
            {
                eventoItem.Desabilitado = true;
                _context.Entry(eventoItem).Property(e => e.Desabilitado).IsModified = true;

                await _context.SaveChangesAsync();
            }
        }
     
    }
}
