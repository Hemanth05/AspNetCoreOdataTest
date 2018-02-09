using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreOdataTest.Model;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreOdataTest.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private static List<BusinessOrder> OrderList = new List<BusinessOrder>
        {
            new BusinessOrder { OrderId = 11, OrderName = "Order1", OrderQuantity = 1 },
            new BusinessOrder { OrderId = 33, OrderName = "Order3", OrderQuantity = 3 },
            new BusinessOrder { OrderId = 4,  OrderName = "Order4", OrderQuantity = 100 },
            new BusinessOrder { OrderId = 22, OrderName = "Order2", OrderQuantity = 2 },
            new BusinessOrder { OrderId = 3,  OrderName = "Order0", OrderQuantity = 0 },
        };

        public IQueryable<OrderDto> Get(ODataQueryOptions queryOptions)
        {
            var data = OrderList.AsQueryable();
            BusinessOrder.StaticDebugHelper = "before filter applied";
            var ret = (IQueryable<OrderDto>)queryOptions.ApplyTo(data.ProjectTo<OrderDto>());
            BusinessOrder.StaticDebugHelper = "after filter applied";
            return ret;
        }

    }
}
