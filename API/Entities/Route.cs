using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Route
    {
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public string  Origin { get; set; }
        public int  Days { get; set; }
        public string Destination {get; set; }
        public int AvailableSeats { get; set; }
        public string Owner { get; set; }
        public string Notes  { get; set; }
        public DateTime AddedOn { get; set; }
    }
}

