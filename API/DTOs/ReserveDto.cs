using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ReserveDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime DateJoined { get; set; }
        public List<ReserveComuteDto> Comutes { get; set; }
    }
}