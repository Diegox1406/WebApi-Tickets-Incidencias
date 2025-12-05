namespace webApiTickets.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Rol { get; set; } = "Usuario"; // Usuario, Administrador
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    
    public ICollection<Ticket> TicketsCreados { get; set; } = new List<Ticket>();
    public ICollection<Ticket> TicketsAsignados { get; set; } = new List<Ticket>();
}
