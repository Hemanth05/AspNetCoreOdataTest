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
        public IQueryable<OrderGraphDto> Get(ODataQueryOptions queryOptions)
        {
            string _debugMessage = null;
            IEnumerable<OrderEntity> GetOrders()
            {
                yield return new OrderEntity(11, "Order1", 1, _debugMessage, "Budapest");
                yield return new OrderEntity(33, "Order3", 3, _debugMessage, "Győr");
                yield return new OrderEntity(4, "Order4", 100, _debugMessage, "Veszprém");
                yield return new OrderEntity(22, "Order2", 2, _debugMessage, "Gyula");
                yield return new OrderEntity(3, "Order0", 0, _debugMessage, "Pécs");
            }

            var data = GetOrders().AsQueryable();
            _debugMessage = "before filter applied";
            var mappedData = data.ProjectTo<OrderGraphDto>();
            var ret = queryOptions.ApplyToQueryable(mappedData);
            _debugMessage = "after filter applied";
            return ret;
        }

    }
}
