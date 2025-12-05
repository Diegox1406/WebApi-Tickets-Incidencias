using webApiTickets.DTOs;

namespace webApiTickets.Services;

public interface ITicketService
{
    Task<TicketResponseDto> CrearTicket(CrearTicketDto dto, int usuarioId);
    Task<TicketResponseDto?> ObtenerTicket(int id);
    Task<List<TicketResponseDto>> ObtenerTickets();
    Task<TicketResponseDto?> ActualizarTicket(int id, ActualizarTicketDto dto, int usuarioId);
    Task<bool> EliminarTicket(int id);
    Task<List<HistorialResponseDto>> ObtenerHistorial(int ticketId);
}
