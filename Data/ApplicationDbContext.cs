using Microsoft.EntityFrameworkCore;
using webApiTickets.Models;

namespace webApiTickets.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<HistorialTicket> HistorialTickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(2000);
            
            entity.HasOne(e => e.Creador)
                .WithMany(u => u.TicketsCreados)
                .HasForeignKey(e => e.CreadorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(e => e.Responsable)
                .WithMany(u => u.TicketsAsignados)
                .HasForeignKey(e => e.ResponsableId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<HistorialTicket>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(e => e.Ticket)
                .WithMany(t => t.Historial)
                .HasForeignKey(e => e.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
