using System;
using System.Collections.Generic;
using System.Text;

namespace CabinRenter.Domain
{
    public class Week
    {
        public int Id { get; set; }
        public int NoWeek { get; set; }
        public DateTime Year { get; set; }
        public ICollection<RentalObjectWeek> CabinWeeks { get; set; }
        //public ICollection<Booking> Booking { get; set; }


    }
}
