using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CabinRenter.Data;
using CabinRenter.Domain;
using Microsoft.EntityFrameworkCore;

namespace CabinRenter.UI
{
    public class Seeder
    {
        private static CabinContext _context;

        public Seeder(CabinContext context)
        {
            _context = context;
        }

        /**
         * 
         * Valde att göra seed-metoderna Async för att man i teorin skulle
         * kunna köra vidare med programmet om det är mycket data att ladda in.
         *
         */

        public async Task SeedDb(bool seed = true)
        {
            var dict = new Dictionary<string, int>();
            if (seed)
            {

                var objCount = await SeedObjectTypesAsync();
                dict.Add("ObjectTypes added", objCount);

                var weekCount = await SeedWeeksAsync();
                dict.Add("Weeks added", weekCount);

                var personsCount = await SeedPersonsAsync();
                dict.Add("Persons w/ address added", personsCount);

                var rentalObjCount = await SeedRentalObjectsAsync();
                dict.Add("RentalObject w/ address added", rentalObjCount);
                Console.WriteLine("Seeding db...");
            }
            else
            {
                var clearCount = ClearSeededData();
                dict.Add("Rows deleted", clearCount);
            }

            foreach (var item in dict)
            {
                Console.WriteLine(item.Key + ": " + item.Value);
            }



        }

        private static int ClearSeededData()
        {
            var ob = _context.ObjectTypes.Where(x => x.Id > 0).ToList();
            _context.ObjectTypes.RemoveRange(ob);

            var w = _context.Weeks.Where(x => x.Id > 0).ToList();
            _context.Weeks.RemoveRange(w);

            var p = _context.Persons.Where(x => x.Id > 0).ToList();
            _context.Persons.RemoveRange(p);

            var a = _context.Addresses.Where(x => x.Id > 0).ToList();
            _context.Addresses.RemoveRange(a);
            var i = _context.SaveChanges();


            //Reset Ids just so Re-seed will work with same Ids..
            _context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Persons', RESEED, 0)");
            _context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Weeks', RESEED, 0)");
            _context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('ObjectTypes', RESEED, 0)");
            _context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('RentalObjects', RESEED, 0)");

            return i;
        }


        private async Task<int> SeedObjectTypesAsync()
        {
            if (_context.ObjectTypes.Any())
                return 0;

            var types = new List<ObjectType>
            {
                new ObjectType { Type = "Cabin" },
                new ObjectType { Type = "Cottage" },
                new ObjectType { Type = "Villa" },
                new ObjectType { Type = "Apartment" }
            };

            _context.AddRange(types);
            return await _context.SaveChangesAsync();
        }

        private async Task<int> SeedWeeksAsync()
        {
            if (_context.Weeks.Any())
                return 0;

            var weeks = new List<Week>();

            for (int y = 2018; y <= 2019; y++)
            {
                for (int i = 1; i <= 53; i++)
                {

                    weeks.Add(new Week
                    {
                        NoWeek = i,
                        Year = new DateTime(y, 1, 1)
                    });

                    if (y == 2019 && i == 52)
                    {
                        i = 54;
                    }
                }
            }

            _context.Weeks.AddRange(weeks);
            return await _context.SaveChangesAsync();
        }

        private async Task<int> SeedPersonsAsync()
        {
            if (_context.Persons.Any())
                return 0;

            var persons = new List<Person>
            {
                new Person
                {
                    FirstName = "Gunnar",
                    LastName = "Gren",
                    Address = new Address
                    {
                        StreetAddress = "Gatan 1",
                        ZipCode = "41101",
                        City = "Gothenburg",
                        Country ="Sweden",
                    }
                },
                new Person
                {
                    FirstName = "Kungen Carl-Gustav",
                    LastName = "Bernadotte",
                    Address = new Address
                    {
                        StreetAddress = "Slottet 1",
                        ZipCode = "slott1",
                        City ="Stockholm",
                        Country ="Sweden"
                    }
                },
                new Person
                {
                    FirstName = "Charlotte",
                    LastName = "Kalla",
                    Address = new Address
                    {
                        StreetAddress ="Skidvägen 1",
                        ZipCode ="skistar",
                        City ="Mora",
                        Country ="Sweden"
                    }
                },
                new Person
                {
                    FirstName ="Beyonce",
                    LastName ="Artist",
                    Address = new Address
                    {
                        StreetAddress ="155 Mulholland Dr",
                        ZipCode ="55533",
                        City ="Beverly Hills",
                        Country ="USA"
                    }
                }
            };

            _context.Persons.AddRange(persons);
            return await _context.SaveChangesAsync();
        }

        private async Task<int> SeedRentalObjectsAsync()
        {
            if (_context.RentalObjects.Any())
                return 0;

            var ro = new List<RentalObject>
            {
                new RentalObject
                {
                    Description = "nice wooden cabin in the mountains",
                    ObjectTypeId = 1,
                    Address = new Address
                    {
                        StreetAddress ="stugvägen 1",
                        ZipCode = "12345",
                        City = "Sälen",
                        Country = "Sweden"
                    },
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Title = "the Cabin",
                            Path = "images/the_cabin.jpg",
                            Order = 1
                        },
                        new Photo
                        {
                            Title = "the mountain",
                            Path = "images/mountain.jpg",
                            Order = 2
                        }
                    },
                    AvailableWeeks = new List<RentalObjectWeek> {
                        new RentalObjectWeek
                        {
                            WeekId = 73,
                            Price = 10000,

                        },
                        new RentalObjectWeek
                        {
                            WeekId = 74,
                            Price = 10000,

                        }
                    }
                },
                new RentalObject
                {
                    Description = "Summerhouse on the beach",
                    ObjectTypeId = 3,
                    Address = new Address
                    {
                        StreetAddress = "Miami beach 1",
                        ZipCode = "Miami",
                        City = "Miami",
                        Country = "USA"
                    },
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Title = "the beach",
                            Path = "images/the_beach.jpg",
                            Order = 1
                        }
                    },
                    AvailableWeeks = new List<RentalObjectWeek> {
                        new RentalObjectWeek
                        {
                            WeekId = 73,
                            Price = 10000,

                        },
                        new RentalObjectWeek
                        {
                            WeekId = 74,
                            Price = 10000,

                        }
                    }
                },
                 new RentalObject
                {
                    Description = "Apartment on the beach",
                    ObjectTypeId = 3,
                    Address = new Address
                    {
                        StreetAddress = "Miami beach 144",
                        ZipCode = "Miami",
                        City = "Miami",
                        Country = "USA"
                    },
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Title = "the beach2",
                            Path = "images/the_beach2.jpg",
                            Order = 1
                        }
                    },
                    AvailableWeeks = new List<RentalObjectWeek> {
                        new RentalObjectWeek
                        {
                            WeekId = 73,
                            Price = 10000,
                            
                        },
                        new RentalObjectWeek
                        {
                            WeekId = 74,
                            Price = 10000,

                        }
                    }
                },
                 new RentalObject
                {
                    Description = "Apartmentin miami",
                    ObjectTypeId = 3,
                    Address = new Address
                    {
                        StreetAddress = "Miami road 14",
                        ZipCode = "Miami",
                        City = "Miami",
                        Country = "USA"
                    },
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Title = "the apartment",
                            Path = "images/apartment.jpg",
                            Order = 1
                        }
                    },
                    AvailableWeeks = new List<RentalObjectWeek> {
                        new RentalObjectWeek
                        {
                            WeekId = 71,
                            Price = 10000,
                            Booking = new Booking
                            {
                                PersonId = 2
                            }

                        },
                        new RentalObjectWeek
                        {
                            WeekId = 72,
                            Price = 10000,

                        },
                        new RentalObjectWeek
                        {
                            WeekId = 73,
                            Price = 10000,

                        }
                    }
                }

            };
            _context.RentalObjects.AddRange(ro);
            return await _context.SaveChangesAsync();
        }

    }
}
