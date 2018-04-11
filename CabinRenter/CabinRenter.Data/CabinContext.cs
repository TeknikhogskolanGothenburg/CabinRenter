using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CabinRenter.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace CabinRenter.Data
{
    public class CabinContext : DbContext
    {


        public DbSet<CabinRenter.Domain.Address> Addresses { get; set; }
        public DbSet<CabinRenter.Domain.Booking> Bookings { get; set; }
        public DbSet<CabinRenter.Domain.ObjectType> ObjectTypes { get; set; }
        public DbSet<CabinRenter.Domain.Person> Persons { get; set; }
        public DbSet<CabinRenter.Domain.Photo> Photos { get; set; }
        public DbSet<CabinRenter.Domain.RentalObject> RentalObjects { get; set; }
        public DbSet<CabinRenter.Domain.Week> Weeks { get; set; }

        public static readonly LoggerFactory CabinLoggerFactory
            = new LoggerFactory(new[]
            {
                new ConsoleLoggerProvider((category, level)
                    => category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information, true)
            });

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
                            entry.CurrentValues["LastUpdatedAt"] = now;
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
            modelBuilder.Entity<RentalObjectWeek>().HasKey(x => new { x.RentalObjectId, x.WeekId });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                .Property(e => e.CreatedAt)
                .HasField("_createdAt");

            modelBuilder.Entity<Booking>()
                .Property(e => e.LastUpdatedAt)
                .HasField("_lastUpdatedAt");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                //.UseLoggerFactory(CabinLoggerFactory)
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CabinRenterDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }


    }
}
