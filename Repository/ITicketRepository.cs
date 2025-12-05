using webApiTickets.Models;

namespace webApiTickets.Repositories;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<Ticket?> GetByIdWithRelationsAsync(int id);
    Task<IEnumerable<Ticket>> GetAllWithRelationsAsync();
    Task<IEnumerable<Ticket>> GetByEstadoAsync(EstadoTicket estado);
    Task<IEnumerable<Ticket>> GetByResponsableAsync(int responsableId);
}
