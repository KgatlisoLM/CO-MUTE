using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Extensions
{
    public static class RouteExtensions
    {
        public static IQueryable<Route> Sort(this IQueryable<Route>query, string orderBy)
        {
            if(string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(d => d.AddedOn);

            query =  orderBy switch {
             _=> query.OrderBy(d => d.AddedOn)
            };

            return query;
        }

        public static IQueryable<Route> Search(this IQueryable<Route> query, string searchTerm){
           
            if (string.IsNullOrEmpty(searchTerm)) return query;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return query.Where(r => r.Destination.ToLower().Contains(lowerCaseSearchTerm));
        }
    }
}