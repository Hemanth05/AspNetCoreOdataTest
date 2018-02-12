using System.Linq;
using Microsoft.AspNet.OData.Query;

namespace AspNetCoreOdataTest.Extensions
{
    public static class ODataQueryOptionsExtensions
    {
        public static IQueryable<T> ApplyToQueryable<T>(this ODataQueryOptions queryOptions, IQueryable<T> data)
        {
            var ret = queryOptions.ApplyTo(data);
            //var list = ret.Cast<T>().ToList();
            return (IQueryable<T>)ret;
        }
    }
}
