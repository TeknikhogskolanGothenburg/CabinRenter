using System;
using System.Collections.Generic;
using System.Linq;
using CabinRenter.Data;
using CabinRenter.Domain;
using Microsoft.EntityFrameworkCore;

namespace CabinRenter.UI
{
    class Program
    {
        static void Main(string[] args)
        {

            //var s = new Seeder(new CabinContext());
            //s.SeedDb(false);
            // use s.SeedDb(false); to re-seed and reset Ids in db
            string key="";

            while (key != "q")
            {
                
                Console.WriteLine("Press number + <Enter>:");
                Console.WriteLine("\t1: to Seed Db");
                Console.WriteLine("\t2: to clear posts in Db");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("\t3: to view all rental objects with only 1 photo");
                Console.WriteLine("\t4: to search for available rental objects by city");
                Console.WriteLine("\t5: Add booking");
                Console.WriteLine("\t6: Get All Bookings");
                Console.WriteLine("\t7: Add available week for rent");
                Console.WriteLine("\t8: Add available weeks for rent");
                Console.WriteLine("\t9: Delete 1 person");
                Console.WriteLine("\t10: Get booking top list");
                Console.WriteLine("\tq: quit");

                key = Console.ReadLine();

                

                var s = new Seeder(new CabinContext());
                switch (key)
                {
                    case "1":
                        s.SeedDb();
                        Console.ReadLine();
                        break;
                    case "2":
                        s.SeedDb(false);
                        Console.ReadLine();
                        break;
                    case "3":
                        GetAllRentalObjectsWithOnePhoto();
                        Console.ReadLine();
                        break;
                    case "4":
                        Console.Write("Address search term (city or streetadress): ");
                        var str = Console.ReadLine();
                        SearchAvailableObjectsByAddress(str);
                        Console.ReadLine();
                        break;
                    case "5":
                        AddBooking();
                        Console.ReadLine();
                        break;
                    case "6":
                        GetAllBookings();
                        break;
                    case "7":
                        AddAvailableWeekToObject();
                        break;
                    case "8":
                        AddAvailableWeeksToObject();
                        break;
                    case "9":
                        DeletePerson();
                        break;
                    
                    case "q":
                        break;
                    default:
                        break;
                }
            }
        }


        private static void GetAllRentalObjectsWithOnePhoto()
        {
            Console.WriteLine("\n\nRental objects");

            using(var context = new CabinContext())
            {
                var cabins = (from r in context.RentalObjects
                              join p in context.Photos on r.Id equals p.RentalObjectId
                              join a in context.Addresses on r.AddressId equals a.Id
                              where r.Photos.Count == 1
                              select new {
                                  r,
                                  p,
                                  a
                              })
                           .ToList();


                foreach (var cabin in cabins)
                {
                    var weekStr = "";
                    var o = cabin.r;

                    Console.WriteLine("\n\n" + o.Description);
                    Console.WriteLine(o.Address.StreetAddress + ", " + o.Address.City);
                    Console.WriteLine("Number of Photos: " + o.Photos.Count());

                }
            }           
        }


        private static void SearchAvailableObjectsByAddress(string searchStr)
        {
            var context = new CabinContext();
            searchStr = searchStr.ToLower();
            
            var cabins = context.RentalObjectWeeks
                .Where(x => x.BookingId == null)
                .Where(x => x.RentalObject.Address.City.Contains(searchStr))
                .Include(x => x.RentalObject)
                    .ThenInclude(x=>x.Address)
                .Include(x=>x.Week)
                .OrderBy(x=>x.Week)
                .GroupBy(x=>x.Week.NoWeek)
                .ToList();

            if(cabins != null)
            {
                Console.WriteLine("\n\nAvailable Weeks & Cabins");
            }
            
            foreach (var cabin in cabins)
            {
               
                foreach(var obj in cabin)
                {
                    var c = obj.RentalObject;
                    Console.WriteLine("\nWeek: " + obj.Week.NoWeek + " " + obj.Week.Year.ToString("yyyy"));
                    Console.WriteLine(c.Description);
                    Console.WriteLine("Address: " + c.Address.StreetAddress + ", " + c.Address.City);
                    
                }
            }   
        }

        private static void DeletePerson()
        {
            //parameters
            var id = 4;

            var context = new CabinContext();

            var person = context.Persons.Find(4);
            context.Persons.Remove(person);
            context.SaveChanges();
            
        }


        private static void AddBooking()
        {
            //parameters
            var weekId = 74;
            var rentalObjectId = 1;


            var context = new CabinContext();

            var person = context.Persons.Where(x => x.LastName == "Bernadotte").FirstOrDefault();
            
            var rObj = context.RentalObjectWeeks
                .Include(x => x.RentalObject)
                .Where(x => x.Booking == null && x.WeekId == weekId && x.RentalObjectId == rentalObjectId).FirstOrDefault();

            
            rObj.Booking = new Booking
            {
                Person = person
            };
            
            var i = context.SaveChanges();
            Console.WriteLine(String.Format("Added {0} booking", i));
        }


        private static void GetAllBookings()
        {
            var context = new CabinContext();

            var bookings = context.Bookings
                .Include(x => x.BookedWeeks)
                    .ThenInclude(x=>x.RentalObject)
                .Include(x=>x.BookedWeeks)
                    .ThenInclude(x=>x.Week)
                .Include(x=>x.Person)
                .ToList();

            foreach(var b in bookings)
            {
                Console.WriteLine("\nBookingId: " + b.Id);
                Console.WriteLine(b.Person.FirstName + " " + b.Person.LastName);
                foreach(var bw in b.BookedWeeks)
                {
                    Console.WriteLine(bw.RentalObject.Description);
                    Console.WriteLine("Week: " + bw.Week.NoWeek + " " + bw.Week.Year.ToString("yyyy"));
                    Console.WriteLine(bw.Price + "SEK");
                }
            }
        }


        private static void AddAvailableWeekToObject()
        {
            //Parameters
            var wId = 78;


            var context = new CabinContext();

            var obj = context.RentalObjects
                .Where(x => x.Id == 2)
                .Include(x => x.AvailableWeeks)
                .Single();

            obj.AvailableWeeks.Add(new RentalObjectWeek
            {
                WeekId = wId,
            });

            var i = context.SaveChanges();
            Console.WriteLine(String.Format("Added {0} week available for rent", i));
        }


        private static void AddAvailableWeeksToObject()
        {
            //Parameters
            var wId = new List<int> { 79, 80, 81, 82 };


            var context = new CabinContext();

            var obj = context.RentalObjects
                .Where(x => x.Id == 2)
                .Include(x => x.AvailableWeeks)
                .Single();

            

            foreach(var w in wId)
            {
                obj.AvailableWeeks.Add(new RentalObjectWeek
                {
                    WeekId = w,
                });
            }
            

            var i = context.SaveChanges();
            Console.WriteLine(String.Format("Added {0} weeks available for rent", i));
        }
        

    }
}
