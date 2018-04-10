using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CabinRenter.Domain;
using Microsoft.EntityFrameworkCore;

namespace CabinRenter.Data
{
    public class CabinContext : DbContext
    {

        public CabinContext(DbContextOptions<CabinContext> options) : base(options) { }
        public CabinContext() { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ObjectType> ObjectTypes { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<RentalObject> RentalObjects { get; set; }
        public DbSet<Week> Weeks { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is Booking booking)
                {
                    var now = DateTime.UtcNow;
                   
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.CurrentValues["LastUpdatedAt"] = now;;
                            break;

                        case EntityState.Added:
                            entry.CurrentValues["CreatedAt"] = now;
                            entry.CurrentValues["LastUpdatedAt"] = now;
                            break;
                    }
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RentalObjectWeek>().HasKey(x => new { x.RentalObjectId, x.WeekId});
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                .Property(post => post.CreatedAt)
                .HasField("_createdAt");

            modelBuilder.Entity<Booking>()
                .Property(post => post.LastUpdatedAt)
                .HasField("_lastUpdatedAt");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=EROL;Initial Catalog=CabinRenterDb;Integrated Security=True;Trusted Connection=True");
        }


    }
}
