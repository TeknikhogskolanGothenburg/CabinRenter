using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CabinRenter.Data;
using CabinRenter.Domain;

namespace CabinRenter.UI
{
    public class Seeder
    {
        private static CabinContext _context;

        public Seeder(CabinContext context)
        {
            _context = context;
        }

        public static async Task SeedDb()
        {

            await SeedObjectTypes();
            await SeedWeeks();


            if (_context.RentalObjects.Any())
            {
                return;
            }

            var data = new RentalObject
            {
                Description = "Finfin stuga på "
            };
            


        }

        private static async Task SeedObjectTypes()
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

            await _context.AddRangeAsync(types);
        }

        private static async Task SeedWeeks()
        {
            if (_context.Weeks.Any())
                return;

            var weeks = new List<Week>();

            for (int y = 2018; y <=2019; y++)
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

            await _context.Weeks.AddRangeAsync(weeks);           
        }

        private static async Task SeedPersons()
        {
            if (_context.Weeks.Any())
                return;

            var persons = new List<Person>
            {
                new Person
                {
                    FirstName = "Gunnar",
                    LastName = "Gren",
                    
                    
                }
            };


        }


    }
}
