using Microsoft.EntityFrameworkCore;
using webApiTickets.Data;
using webApiTickets.Models;

namespace webApiTickets.Repositories;

public class TicketRepository : Repository<Ticket>, ITicketRepository
{
    public TicketRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Ticket?> GetByIdWithRelationsAsync(int id)
    {
        return await _dbSet
            .Include(t => t.Creador)
            .Include(t => t.Responsable)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Ticket>> GetAllWithRelationsAsync()
    {
        return await _dbSet
            .Include(t => t.Creador)
            .Include(t => t.Responsable)
            .OrderByDescending(t => t.FechaCreacion)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByEstadoAsync(EstadoTicket estado)
    {
        return await _dbSet
            .Include(t => t.Creador)
            .Include(t => t.Responsable)
            .Where(t => t.Estado == estado)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByResponsableAsync(int responsableId)
    {
        return await _dbSet
            .Include(t => t.Creador)
            .Include(t => t.Responsable)
            .Where(t => t.ResponsableId == responsableId)
            .ToListAsync();
    }
}
