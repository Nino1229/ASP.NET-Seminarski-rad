using ASP.NET_Seminarski_rad.Data;
using ASP.NET_Seminarski_rad.Extensions;
using ASP.NET_Seminarski_rad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ASP.NET_Seminarski_rad.Controllers
{
    public class CartController : Controller
    {
        private readonly string _sessionKeyName = "_cart";
        private ApplicationDbContext _dbContext;

        public CartController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sessionKeyName);

            if (cart == null) cart = new List<CartItem>();

            decimal sum = 0;
            cart.Sum(item => sum += item.GetTotal());
            ViewBag.TotalPrice = sum;

            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            List<CartItem> cartItems = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sessionKeyName);

            if (cartItems == null) cartItems = new List<CartItem>();

            if (cartItems.Count == 0)
            {
                var product = _dbContext.Product.FirstOrDefault(p => p.Id == productId);
                CartItem cartItem = new CartItem()
                {
                    Product = product,
                    Quantity = 1
                };

                cartItems.Add(cartItem);

                HttpContext.Session.SetObjectAsJson(_sessionKeyName, cartItems);
            }

            else
            {
                int result = IsExistingInCart(productId);

                if (result == -1)
                {
                    var product = _dbContext.Product.FirstOrDefault(p => p.Id == productId);

                    CartItem cartItem = new CartItem()
                    {
                        Product = product,
                        Quantity = 1
                    };

                    cartItems.Add(cartItem);
                }
                else
                {
                    cartItems[result].Quantity++;
                }

                HttpContext.Session.SetObjectAsJson(_sessionKeyName, cartItems);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int productId)
        {
            List<CartItem> cartItems = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sessionKeyName);
            if (cartItems == null) cartItems = new List<CartItem>();
            int result = IsExistingInCart(productId);
            cartItems.RemoveAt(result);

            HttpContext.Session.SetObjectAsJson(_sessionKeyName, cartItems);

            return RedirectToAction(nameof(Index));

        }

        private int IsExistingInCart(int productId)
        {
            List<CartItem> cartItems = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sessionKeyName);
            if (cartItems == null) cartItems = new List<CartItem>();
            
            for (int i = 0; i < cartItems.Count; i++)
            {
                if (cartItems[i].Product.Id == productId)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
