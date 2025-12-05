using webApiTickets.Models;

namespace webApiTickets.DTOs;

public class CrearTicketDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public TipoTicket Tipo { get; set; }
    public PrioridadTicket Prioridad { get; set; }
}

public class ActualizarTicketDto
{
    public string? Titulo { get; set; }
    public string? Descripcion { get; set; }
    public EstadoTicket? Estado { get; set; }
    public PrioridadTicket? Prioridad { get; set; }
    public int? ResponsableId { get; set; }
    public string? Comentario { get; set; }
}

public class TicketResponseDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Prioridad { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Creador { get; set; } = string.Empty;
    public string? Responsable { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public DateTime? FechaCierre { get; set; }
}

public class HistorialResponseDto
{
    public int Id { get; set; }
    public string Accion { get; set; } = string.Empty;
    public string? ValorAnterior { get; set; }
    public string? ValorNuevo { get; set; }
    public string? Comentario { get; set; }
    public string Usuario { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
}
