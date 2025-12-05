namespace webApiTickets.Repositories;

public interface IUnitOfWork : IDisposable
{
    ITicketRepository Tickets { get; }
    IUsuarioRepository Usuarios { get; }
    IHistorialRepository Historial { get; }
    Task<int> SaveChangesAsync();
}
