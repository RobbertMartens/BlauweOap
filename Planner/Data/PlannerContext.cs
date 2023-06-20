using Microsoft.EntityFrameworkCore;
using Planner.Models;

namespace Planner.Data
{
    public class PlannerContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<PartyPeople> PartyPeople { get; set; }

        public PlannerContext(DbContextOptions<PlannerContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>().ToTable("Reservations");
            modelBuilder.Entity<PartyPeople>().ToTable("PartyPeople");
        }
    }
}
