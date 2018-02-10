using System.Linq;
using Microsoft.AspNet.OData.Query;

namespace AspNetCoreOdataTest.Extensions
{
    public static class ODataQueryOptionsExtensions
    {
        public static IQueryable<T> ApplyToQueryable<T>(this ODataQueryOptions queryOptions, IQueryable<T> data)
        {
            return (IQueryable<T>)queryOptions.ApplyTo(data);
        }
    }
}
