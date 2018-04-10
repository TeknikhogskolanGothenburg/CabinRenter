using System;
using System.Collections.Generic;
using System.Text;

namespace CabinRenter.Domain
{
    public class RentalObject
    {

        public RentalObject()
        {
            
        }

        public int Id { get; set; }                
        public string Description { get; set; }
        public Address Address { get; set; }
        public int PersonId { get; set; }
        public Person Owner { get; set; }
        public int ObjectTypeId { get; set; }
        public ObjectType ObjectType { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<RentalObjectWeek> AvailableWeeks { get; set; }
        





    }
}
