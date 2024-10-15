using Microsoft.AspNetCore.Mvc;
using PruebaNexTiVentaEntrada.Modelos;
using PruebaNexTiVentaEntrada.Repositorios.Contrato;

namespace PruebaNexTiVentaEntrada.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : Controller
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoController(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Evento>> GetEventos()
        {
            return await _eventoRepository.GetAllEventos();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> GetEvento(int id)
        {
            var eventoItem = await _eventoRepository.GetEventoById(id);
            if (eventoItem == null)
            {
                return NotFound();
            }

            return eventoItem;
        }

        [HttpPost]
        public async Task<ActionResult> CreateEvent(Evento nuevoEvento)
        {
            await _eventoRepository.AddEvento(nuevoEvento);
            return CreatedAtAction(nameof(GetEvento), new { id = nuevoEvento.Id }, nuevoEvento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Evento updatedEvent)
        {
            if (id != updatedEvent.Id)
            {
                return BadRequest();
            }

            await _eventoRepository.UpdateEvento(updatedEvent);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            await _eventoRepository.DeleteEvento(id);
            return NoContent();
        }
    }
}
