using webApiTickets.DTOs;
using webApiTickets.Models;
using webApiTickets.Repositories;

namespace webApiTickets.Services;

public class TicketService : ITicketService
{
    private readonly IUnitOfWork _unitOfWork;

    public TicketService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TicketResponseDto> CrearTicket(CrearTicketDto dto, int usuarioId)
    {
        var ticket = new Ticket
        {
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            Tipo = dto.Tipo,
            Prioridad = dto.Prioridad,
            CreadorId = usuarioId,
            Estado = EstadoTicket.Abierto
        };

        await _unitOfWork.Tickets.AddAsync(ticket);
        await _unitOfWork.SaveChangesAsync();

        await RegistrarHistorial(ticket.Id, "Creación", null, "Ticket creado", usuarioId);

        return await ObtenerTicket(ticket.Id) ?? throw new Exception("Error al crear ticket");
    }

    public async Task<TicketResponseDto?> ObtenerTicket(int id)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdWithRelationsAsync(id);
        if (ticket == null) return null;

        return MapearTicketResponse(ticket);
    }

    public async Task<List<TicketResponseDto>> ObtenerTickets()
    {
        var tickets = await _unitOfWork.Tickets.GetAllWithRelationsAsync();
        return tickets.Select(MapearTicketResponse).ToList();
    }

    public async Task<TicketResponseDto?> ActualizarTicket(int id, ActualizarTicketDto dto, int usuarioId)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
        if (ticket == null) return null;

        if (!string.IsNullOrEmpty(dto.Titulo))
        {
            await RegistrarHistorial(id, "Título actualizado", ticket.Titulo, dto.Titulo, usuarioId, dto.Comentario);
            ticket.Titulo = dto.Titulo;
        }

        if (!string.IsNullOrEmpty(dto.Descripcion))
        {
            await RegistrarHistorial(id, "Descripción actualizada", null, null, usuarioId, dto.Comentario);
            ticket.Descripcion = dto.Descripcion;
        }

        if (dto.Estado.HasValue && dto.Estado.Value != ticket.Estado)
        {
            await RegistrarHistorial(id, "Estado cambiado", ticket.Estado.ToString(), dto.Estado.Value.ToString(), usuarioId, dto.Comentario);
            ticket.Estado = dto.Estado.Value;
            
            if (dto.Estado.Value == EstadoTicket.Cerrado)
                ticket.FechaCierre = DateTime.UtcNow;
        }

        if (dto.Prioridad.HasValue && dto.Prioridad.Value != ticket.Prioridad)
        {
            await RegistrarHistorial(id, "Prioridad cambiada", ticket.Prioridad.ToString(), dto.Prioridad.Value.ToString(), usuarioId, dto.Comentario);
            ticket.Prioridad = dto.Prioridad.Value;
        }

        if (dto.ResponsableId.HasValue && dto.ResponsableId.Value != ticket.ResponsableId)
        {
            var responsableAnterior = ticket.ResponsableId?.ToString() ?? "Sin asignar";
            var responsableNuevo = dto.ResponsableId.Value.ToString();
            await RegistrarHistorial(id, "Responsable asignado", responsableAnterior, responsableNuevo, usuarioId, dto.Comentario);
            ticket.ResponsableId = dto.ResponsableId.Value;
        }

        ticket.FechaActualizacion = DateTime.UtcNow;
        _unitOfWork.Tickets.Update(ticket);
        await _unitOfWork.SaveChangesAsync();

        return await ObtenerTicket(id);
    }

    public async Task<bool> EliminarTicket(int id)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
        if (ticket == null) return false;

        _unitOfWork.Tickets.Remove(ticket);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<List<HistorialResponseDto>> ObtenerHistorial(int ticketId)
    {
        var historial = await _unitOfWork.Historial.GetByTicketIdAsync(ticketId);

        return historial.Select(h => new HistorialResponseDto
        {
            Id = h.Id,
            Accion = h.Accion,
            ValorAnterior = h.ValorAnterior,
            ValorNuevo = h.ValorNuevo,
            Comentario = h.Comentario,
            Usuario = h.Usuario.Nombre,
            Fecha = h.Fecha
        }).ToList();
    }

    private async Task RegistrarHistorial(int ticketId, string accion, string? valorAnterior, string? valorNuevo, int usuarioId, string? comentario = null)
    {
        var historial = new HistorialTicket
        {
            TicketId = ticketId,
            Accion = accion,
            ValorAnterior = valorAnterior,
            ValorNuevo = valorNuevo,
            Comentario = comentario,
            UsuarioId = usuarioId
        };

        await _unitOfWork.Historial.AddAsync(historial);
        await _unitOfWork.SaveChangesAsync();
    }

    private static TicketResponseDto MapearTicketResponse(Ticket ticket)
    {
        return new TicketResponseDto
        {
            Id = ticket.Id,
            Titulo = ticket.Titulo,
            Descripcion = ticket.Descripcion,
            Estado = ticket.Estado.ToString(),
            Prioridad = ticket.Prioridad.ToString(),
            Tipo = ticket.Tipo.ToString(),
            Creador = ticket.Creador.Nombre,
            Responsable = ticket.Responsable?.Nombre,
            FechaCreacion = ticket.FechaCreacion,
            FechaActualizacion = ticket.FechaActualizacion,
            FechaCierre = ticket.FechaCierre
        };
    }
}
