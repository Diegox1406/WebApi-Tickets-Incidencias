namespace webApiTickets.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public EstadoTicket Estado { get; set; } = EstadoTicket.Abierto;
    public PrioridadTicket Prioridad { get; set; } = PrioridadTicket.Media;
    public TipoTicket Tipo { get; set; } = TipoTicket.Error;
    
    public int CreadorId { get; set; }
    public Usuario Creador { get; set; } = null!;
    
    public int? ResponsableId { get; set; }
    public Usuario? Responsable { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaActualizacion { get; set; }
    public DateTime? FechaCierre { get; set; }
    
    public ICollection<HistorialTicket> Historial { get; set; } = new List<HistorialTicket>();
}

public enum EstadoTicket
{
    Abierto = 1,
    EnProgreso = 2,
    Cerrado = 3
}

public enum PrioridadTicket
{
    Baja = 1,
    Media = 2,
    Alta = 3,
    Critica = 4
}

public enum TipoTicket
{
    Error = 1,
    Solicitud = 2,
    Consulta = 3,
    Mejora = 4
}
