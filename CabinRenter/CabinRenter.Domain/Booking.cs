using System;
using System.Collections.Generic;
using System.Text;

namespace CabinRenter.Domain
{
    public class Booking
    {
        private DateTime _createdAt;
        
        private DateTime _lastUpdatedAt;

        public int Id { get; set; }
        public ICollection<RentalObjectWeek> BookedWeeks { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public DateTime CreatedAt => _createdAt;
        public DateTime LastUpdatedAt => _lastUpdatedAt;


    }
}
