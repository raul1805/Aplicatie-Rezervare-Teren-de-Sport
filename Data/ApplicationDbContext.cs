using Microsoft.EntityFrameworkCore;
using SportsReservationApp.Models;

namespace SportsReservationApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SportField> SportFields { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
