using PruebaNexTiVentaEntrada.Modelos;

namespace PruebaNexTiVentaEntrada.Repositorios.Contrato
{
    public interface IEventoRepository
    {
        Task<IEnumerable<Evento>> GetAllEventos();
        Task<Evento> GetEventoById(int id);
        Task AddEvento(Evento nuevoEvento);
        Task UpdateEvento(Evento eventToUpdate);
        Task DeleteEvento(int id);
    }
}
