using webApiTickets.Models;

namespace webApiTickets.Repositories;

public interface IHistorialRepository : IRepository<HistorialTicket>
{
    Task<IEnumerable<HistorialTicket>> GetByTicketIdAsync(int ticketId);
}
