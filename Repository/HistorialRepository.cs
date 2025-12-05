using Microsoft.EntityFrameworkCore;
using webApiTickets.Data;
using webApiTickets.Models;

namespace webApiTickets.Repositories;

public class HistorialRepository : Repository<HistorialTicket>, IHistorialRepository
{
    public HistorialRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<HistorialTicket>> GetByTicketIdAsync(int ticketId)
    {
        return await _dbSet
            .Include(h => h.Usuario)
            .Where(h => h.TicketId == ticketId)
            .OrderByDescending(h => h.Fecha)
            .ToListAsync();
    }
}
