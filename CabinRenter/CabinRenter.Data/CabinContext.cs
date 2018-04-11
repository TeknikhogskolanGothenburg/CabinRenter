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


        public DbSet<Address> Addresses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ObjectType> ObjectTypes { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<RentalObject> RentalObjects { get; set; }
        public DbSet<Week> Weeks { get; set; }

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

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Address)
                .WithOne(a => a.Person)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RentalObject>()
                .HasOne(ro => ro.Address)
                .WithOne(a => a.RentalObject)
                .OnDelete(DeleteBehavior.Cascade);


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
                //.EnableSensitiveDataLogging()
                //.UseLoggerFactory(CabinLoggerFactory)
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CabinRenterDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }


    }
}
