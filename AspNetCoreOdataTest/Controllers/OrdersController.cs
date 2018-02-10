using System.Collections.Generic;
using System.Linq;
using AspNetCoreOdataTest.Extensions;
using AspNetCoreOdataTest.Model;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreOdataTest.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        public IQueryable<OrderDto> Get(ODataQueryOptions queryOptions)
        {
            string _debugMessage = null;
            IEnumerable<BusinessOrder> GetOrders()
            {
                yield return new BusinessOrder(11, "Order1", 1, _debugMessage);
                yield return new BusinessOrder(33, "Order3", 3, _debugMessage);
                yield return new BusinessOrder(4, "Order4", 100, _debugMessage);
                yield return new BusinessOrder(22, "Order2", 2, _debugMessage);
                yield return new BusinessOrder(3, "Order0", 0, _debugMessage);
            }

            var data = GetOrders().AsQueryable();
            _debugMessage = "before filter applied";
            var mappedData = data.ProjectTo<OrderDto>();
            var ret = queryOptions.ApplyToQueryable(mappedData);
            _debugMessage = "after filter applied";
            return ret;
        }

    }
}
