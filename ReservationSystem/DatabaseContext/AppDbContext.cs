using Microsoft.EntityFrameworkCore;
using ReservationSystem.Entity;

namespace ReservationSystem.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
