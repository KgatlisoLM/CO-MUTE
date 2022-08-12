using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Reserve 
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public DateTime DateJoined { get; set; } = DateTime.Now;

        public List<ReserveComute> Comutes { get; set; } = new ();


        public void JoinComute(Route route, int qty, DateTime departureTime){
            
            var existingOverlap = Comutes.FirstOrDefault(comute => comute.DepatureTime == departureTime);
            if(existingOverlap != null ) return;

            if (Comutes.All(comute => comute.RouteId != route.Id)){

                Comutes.Add(new ReserveComute{Route = route, Qty = qty});
            }

            var existingComute = Comutes.FirstOrDefault(comute => comute.ReserveId == route.Id);
            if(existingComute != null) existingComute.Qty += qty;
        }

        public void CancelComute(int routeId, int qty){
            
            var comute = Comutes.FirstOrDefault(comute => comute.RouteId == routeId);
            if (comute == null) return;
            comute.Qty -= qty;
            if(comute.Qty == 0) Comutes.Remove(comute);
        }
    }
}