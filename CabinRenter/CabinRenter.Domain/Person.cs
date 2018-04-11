using System;
using System.Collections.Generic;
using System.Text;

namespace CabinRenter.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<Booking> Bookings { get; set; }


    }
}
