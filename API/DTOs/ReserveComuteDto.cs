using System;

namespace API.DTOs
{
    public class ReserveComuteDto
    {
        public int RouteId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public string  Origin { get; set; }
        public int  Days { get; set; }
        public string Destination {get; set; }
        public int AvailableSeats { get; set; }
        public string Notes  { get; set; }
        public DateTime AddedOn { get; set; }
    }
}