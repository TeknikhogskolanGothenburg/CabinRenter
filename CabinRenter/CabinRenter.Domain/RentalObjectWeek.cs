using System;
using System.Collections.Generic;
using System.Text;

namespace CabinRenter.Domain
{
    public class RentalObjectWeek
    {
        public int RentalObjectId { get; set; }
        public RentalObject RentalObject { get; set; }
        public int WeekId { get; set; }
        public Week Week { get; set; }
        public int? BookingId { get; set; }
        public Booking Booking { get; set; }
        public double Price { get; set; }

    }
}
