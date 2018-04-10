using System;
using System.Collections.Generic;
using System.Text;

namespace CabinRenter.Domain
{
    public class ObjectType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        //public byte Enabled { get; set; }
        ICollection<RentalObject> RentalObjects { get; set; }
        
    }
}
