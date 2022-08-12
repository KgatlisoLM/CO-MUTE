using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{


    public class ReserveComute
    {
        public int Id {get; set;}
        public int Qty {get;set;}

        public DateTime DepatureTime { get; set; }

        public int RouteId { get; set; }
        public Route Route { get; set; }

        public int ReserveId  { get; set; }
        public Reserve Reserve { get; set; }
    }
}