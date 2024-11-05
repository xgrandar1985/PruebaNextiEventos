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


        public async Task<IEnumerable<Evento>> GetAllEventosBySP()
        {
            var eventos = new List<Evento>();

            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "sp_Listar_Eventos";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var evento = new Evento
                            {
                                Id = reader.GetInt32(0),
                                Fecha = reader.GetDateTime(1),
                                Lugar = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Descripcion = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Precio = reader.GetDecimal(4),
                                Desabilitado = reader.GetBoolean(5)
                            };

                            eventos.Add(evento);
                        }
                    }
                }
            }

            return eventos;
        }


        public async Task AddEventoBySp(Evento evento)
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "sp_Crear_Evento";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Parámetros del procedimiento almacenado
                    var fechaParam = command.CreateParameter();
                    fechaParam.ParameterName = "@Fecha";
                    fechaParam.Value = evento.Fecha;
                    command.Parameters.Add(fechaParam);

                    var lugarParam = command.CreateParameter();
                    lugarParam.ParameterName = "@Lugar";
                    lugarParam.Value = (object)evento.Lugar ?? DBNull.Value;
                    command.Parameters.Add(lugarParam);

                    var descripcionParam = command.CreateParameter();
                    descripcionParam.ParameterName = "@Descripcion";
                    descripcionParam.Value = (object)evento.Descripcion ?? DBNull.Value;
                    command.Parameters.Add(descripcionParam);

                    var precioParam = command.CreateParameter();
                    precioParam.ParameterName = "@Precio";
                    precioParam.Value = evento.Precio;
                    command.Parameters.Add(precioParam);

                    var desabilitadoParam = command.CreateParameter();
                    desabilitadoParam.ParameterName = "@Desabilitado";
                    desabilitadoParam.Value = evento.Desabilitado;
                    command.Parameters.Add(desabilitadoParam);

                    // Ejecutar el comando
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }
}
