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

        public async Task SeedDb()
        {

            ClearSeededData();

            await SeedObjectTypes();
            await SeedWeeks();
            await SeedPersons();
            await SeedRentalObjects();


            //if (_context.RentalObjects.Any())
            //{
            //    return;
            //}
        }

        private static void ClearSeededData()
        {
            var ob = _context.ObjectTypes.Where(x => x.Id > 0).ToList();
            _context.ObjectTypes.RemoveRange(ob);
            _context.SaveChanges();

            var w = _context.Weeks.Where(x => x.Id > 0).ToList();
            _context.Weeks.RemoveRange(w);
            _context.SaveChanges();

            var p = _context.Persons.Where(x => x.Id > 0).ToList();
            _context.Persons.RemoveRange(p);
            _context.SaveChanges();


            var a = _context.Addresses.Where(x => x.Id > 0).ToList();
            _context.Addresses.RemoveRange(a);
            _context.SaveChanges();


        }


        private async Task SeedObjectTypes()
        {
            if (_context.ObjectTypes.Any())
                return;

            var types = new List<ObjectType>
            {
                new ObjectType { Type = "Cabin" },
                new ObjectType { Type = "Cottage" },
                new ObjectType { Type = "Villa" },
                new ObjectType { Type = "Apartment" }
            };

            _context.AddRange(types);
            await _context.SaveChangesAsync();
        }

        private async Task SeedWeeks()
        {
            if (_context.Weeks.Any())
                return;

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
            await _context.SaveChangesAsync();
        }

        private async Task SeedPersons()
        {
            if (_context.Persons.Any())
                return;

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
            await _context.SaveChangesAsync();


        }

        private async Task SeedRentalObjects()
        {
            if (_context.RentalObjects.Any())
                return;

            var ro = new List<RentalObject>
            {
                new RentalObject
                {
                    Description = "nice wooden cabin in the mountains",
                    ObjectTypeId = 9,
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
                    AvailableWeeks = new List<RentalObjectWeek>
                    {
                        new RentalObjectWeek
                        {
                            
                            WeekId = 388,
                            Price = 8000

                        },
                        new RentalObjectWeek
                        {
                            
                            WeekId = 387,
                            Price = 8000

                        },
                        new RentalObjectWeek
                        {
                            
                            WeekId = 386,
                            Price = 7500
                        }
                    }
                    
                }

            };
             _context.RentalObjects.AddRange(ro);
             await _context.SaveChangesAsync();
        }


    }
}
