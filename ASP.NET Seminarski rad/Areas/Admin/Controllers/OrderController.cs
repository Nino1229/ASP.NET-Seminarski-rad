using ASP.NET_Seminarski_rad.Data;
using ASP.NET_Seminarski_rad.Data.Migrations;
using ASP.NET_Seminarski_rad.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Seminarski_rad.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _dbContext;

        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.Order.ToList());
        }

        public IActionResult Details(int id) 
        {
            if (id == 0) return NotFound();

            var order = _dbContext.Order.FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();

            order.OrderItems = (
                from orderItem in _dbContext.OrderItem
                where orderItem.OrderId == order.Id
                select new OrderItem
                {
                    Id = orderItem.Id,
                    OrderId = orderItem.OrderId,
                    ProductId = orderItem.ProductId,
                    ProductTitle = _dbContext.Product.SingleOrDefault(p => p.Id == orderItem.ProductId).ProductTitle,
                    Quantity = orderItem.Quantity,
                    Total = orderItem.Total,
                }
                ).ToList();

            return View(order);
        }
    }
}
