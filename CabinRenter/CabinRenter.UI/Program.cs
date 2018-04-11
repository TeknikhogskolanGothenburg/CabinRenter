using System;
using CabinRenter.Data;

namespace CabinRenter.UI
{
    class Program
    {
        static void Main(string[] args)
        {

            var s = new Seeder(new CabinContext());
            s.SeedDb();


            Console.WriteLine("Hello World!");
            Console.ReadLine();

        }
    }
}
