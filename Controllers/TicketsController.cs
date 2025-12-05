using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webApiTickets.DTOs;
using webApiTickets.Services;

namespace webApiTickets.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TicketResponseDto>>> ObtenerTickets()
    {
        var tickets = await _ticketService.ObtenerTickets();
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketResponseDto>> ObtenerTicket(int id)
    {
        var ticket = await _ticketService.ObtenerTicket(id);
        
        if (ticket == null)
            return NotFound(new { message = "Ticket no encontrado" });

        return Ok(ticket);
    }

    [HttpPost]
    public async Task<ActionResult<TicketResponseDto>> CrearTicket(CrearTicketDto dto)
    {
        var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var ticket = await _ticketService.CrearTicket(dto, usuarioId);
        
        return CreatedAtAction(nameof(ObtenerTicket), new { id = ticket.Id }, ticket);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TicketResponseDto>> ActualizarTicket(int id, ActualizarTicketDto dto)
    {
        var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var ticket = await _ticketService.ActualizarTicket(id, dto, usuarioId);
        
        if (ticket == null)
            return NotFound(new { message = "Ticket no encontrado" });

        return Ok(ticket);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> EliminarTicket(int id)
    {
        var resultado = await _ticketService.EliminarTicket(id);
        
        if (!resultado)
            return NotFound(new { message = "Ticket no encontrado" });

        return NoContent();
    }

    [HttpGet("{id}/historial")]
    public async Task<ActionResult<List<HistorialResponseDto>>> ObtenerHistorial(int id)
    {
        var historial = await _ticketService.ObtenerHistorial(id);
        return Ok(historial);
    }
}
