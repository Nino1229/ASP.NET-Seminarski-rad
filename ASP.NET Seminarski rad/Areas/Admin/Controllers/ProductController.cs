using ASP.NET_Seminarski_rad.Data;
using ASP.NET_Seminarski_rad.Data.Migrations;
using ASP.NET_Seminarski_rad.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Seminarski_rad.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Product.ToList();
            return View(products);
        }

        public IActionResult Details(int id) 
        {
            var product = _dbContext.Product.FirstOrDefault(x => x.Id == id);

            if (id == 0 || id == null || product == null) 
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid) 
            {
                string wwwRootPath = "wwwroot";

                string fileName = product.ProductImageFile.FileName;
                product.ProductImage = fileName;
                string path = wwwRootPath + "/Images/" + fileName;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    product.ProductImageFile.CopyTo(fileStream);
                }

                _dbContext.Product.Add(product);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var product = _dbContext.Product.FirstOrDefault(x=> x.Id == id);

            if(id == 0 || id == null || product == null) 
            { 
                return NotFound(); 
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product) 
        {
            if (ModelState.IsValid) 
            {
                _dbContext.Product.Update(product);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var product = _dbContext.Product.FirstOrDefault(x => x.Id == id);

            if (id == 0 || id == null || product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id) 
        {
            var product = _dbContext.Product.FirstOrDefault(x => x.Id == id);

            if (id == 0 || id == null || product == null)
            {
                return NotFound();
            }

            _dbContext.Product.Remove(product);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}
