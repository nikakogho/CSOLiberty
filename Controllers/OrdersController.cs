using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSOLiberty.Data;
using CSOLiberty.Models;
using System.Collections;

namespace CSOLiberty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        private object OrderToJson(Order o)
        {
            return new
            {
                id = o.ID,
                parts = o.Children.Count + 1,
                date = o.LastDate.ToString().Substring(0, 10),
                money = o.TotalAmount
            };
        }

        [HttpGet]
        public IEnumerable Get()
        {
            var orders = _context.Orders;

            var mainOrders = orders.Where(o => o.ParentID == null);

            var jsonEnumerable = mainOrders.Select(OrderToJson);
            
            return jsonEnumerable.ToArray();
        }

        [HttpGet("period")]
        public IEnumerable GetInPeriod(DateTime start, DateTime end)
        {
            var orders = _context.Orders.Where(o => o.ParentID == null && o.Date <= end).AsEnumerable();
            var ordersEnum = orders.Where(o => o.IsMain && o.LastDate >= start && o.LastDate <= end);

            var order = ordersEnum.OrderByDescending(o => o.LastDate).FirstOrDefault();

            if (order == null) return new object[0];

            return new object[] { new { id = order.ID, money = order.TotalAmount } };
        }
    }
}
