using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CreateRouteDto
    {
        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ExpectedArrival { get; set; }

        [Required]
        public string  Origin { get; set; }

        [Required]
        public int  Days { get; set; }

        [Required]
        public string Destination {get; set; }

        [Required]
        [Range(0, 200)]
        public int AvailableSeats { get; set; }

        public string Notes  { get; set; }

        [Required]
        public string Owner { get; set; }
        
        [Required]
        public DateTime AddedOn { get; set; }
    }
}