using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuliahZone.Domain.Entity
{
    public class Venue
    {
        public string VenueID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Town { get; set; }
        public string State { get; set; }
        
        public Venue()
        {
            if (String.IsNullOrEmpty(VenueID))
                VenueID = Guid.NewGuid().ToString();
        }
    }
}
