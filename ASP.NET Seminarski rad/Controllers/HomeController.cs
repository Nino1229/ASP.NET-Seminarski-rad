using ASP.NET_Seminarski_rad.Data;
using ASP.NET_Seminarski_rad.Extensions;
using ASP.NET_Seminarski_rad.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace ASP.NET_Seminarski_rad.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _sessionKeyName = "_cart";
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Product(int? categoryId)
        {
            List<Product> products = _dbContext.Product.ToList();

            if (categoryId != null)
            {
                products = (from product in _dbContext.Product
                            join prodCat in _dbContext.ProductCategory on product.Id equals prodCat.ProductId
                            where prodCat.CategoryId == categoryId
                            select new Product
                            {
                                Id = product.Id,
                                ProductTitle = product.ProductTitle,
                                ProductDescription = product.ProductDescription,
                                ProductQuantity = product.ProductQuantity,
                                ProductPrice = product.ProductPrice,
                                ProductImage = product.ProductImage,
                            }).ToList();
            }

            ViewBag.Categories = _dbContext.Category.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.CategoryTitle
            }).ToList();

            return View(products);
        }

        public IActionResult Order(List<string> errors)
        {
            List<CartItem> cartItems = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sessionKeyName);
            if (cartItems == null) cartItems = new List<CartItem>();

            if (cartItems.Count == 0)
            {
                TempData["Message"] = "Morate napunite košaricu kako bi napravili narudžbu!";
                return RedirectToAction(nameof(Index));
            }

            decimal sum = 0;
            ViewBag.TotalPrice = cartItems.Sum(item => sum += item.GetTotal());

            ViewBag.Errors = errors;

            return View(cartItems);

        }

        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            List<CartItem> cartItems = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sessionKeyName);
            if (cartItems == null) cartItems = new List<CartItem>();
            if (cartItems.Count == 0)
            {
                TempData["Message"] = "Morate napunite košaricu kako bi napravili narudžbu!";

                return RedirectToAction(nameof(Index));
            }

            List<string> modelErrors = new List<string>();
            if (ModelState.IsValid)
            {
                var user = _dbContext.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
                if (user != null) order.UserId = user.Id;

                order.DateCreated = DateTime.Now;

                _dbContext.Order.Add(order);
                _dbContext.SaveChanges();

                int orderId = order.Id;

                foreach (var item in cartItems)
                {
                    OrderItem orderItem = new OrderItem()
                    {
                        OrderId = orderId,
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                        Total = item.GetTotal()
                    };

                    _dbContext.OrderItem.Add(orderItem);
                    _dbContext.SaveChanges();
                }

                HttpContext.Session.SetObjectAsJson(_sessionKeyName, string.Empty);

                TempData["Success"] = "Zahvaljujemo vam na vašoj narudžbi!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }
            }

            return RedirectToAction(nameof(Order), new { errors = modelErrors });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
