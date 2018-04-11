using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CabinRenter.Web.Areas.Admin.Models
{
    public class Booking
    {
        
        public int Id { get; set; }
        public List<Week> BookedWeeks { get; set; }
        public Person Person { get; set; }
        public DateTime CreatedAt;
        public DateTime LastUpdatedAt;
    }
}
