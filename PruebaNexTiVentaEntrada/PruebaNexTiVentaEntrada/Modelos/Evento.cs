namespace PruebaNexTiVentaEntrada.Modelos
{
    public class Evento
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string? Lugar { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Desabilitado { get; set; } = false;
    }
}
