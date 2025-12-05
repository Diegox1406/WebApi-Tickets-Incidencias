using webApiTickets.Data;

namespace webApiTickets.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public ITicketRepository Tickets { get; }
    public IUsuarioRepository Usuarios { get; }
    public IHistorialRepository Historial { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Tickets = new TicketRepository(_context);
        Usuarios = new UsuarioRepository(_context);
        Historial = new HistorialRepository(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
