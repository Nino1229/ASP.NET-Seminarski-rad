using ASP.NET_Seminarski_rad.Data;
using ASP.NET_Seminarski_rad.Data.Migrations;
using ASP.NET_Seminarski_rad.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace ASP.NET_Seminarski_rad.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext _dbContext;

        public ProductCategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int productId)
        {
            ViewBag.ProductId = productId;

            var productCategoryList = _dbContext.ProductCategory
                .Where(pc => pc.ProductId == productId)
                .Select(pc => new ProductCategory()
                {
                    Id = pc.Id,
                    ProductId = pc.ProductId,
                    CategoryId = pc.CategoryId,
                    ProductTitle = _dbContext
                    .Product
                    .SingleOrDefault(x => x.Id == pc.ProductId).ProductTitle,
                    CategoryTitle = _dbContext
                    .Category
                    .SingleOrDefault(x => x.Id == pc.CategoryId).CategoryTitle,
                });

            return View(productCategoryList);
        }

        public IActionResult Details(int id)
        {
            if (id == 0) return NotFound();

            var productCategory = _dbContext.ProductCategory
                .SingleOrDefault(pc => pc.Id == id);

            productCategory.ProductTitle = _dbContext
                    .Product
                    .SingleOrDefault(x => x.Id == productCategory.ProductId).ProductTitle;
            productCategory.CategoryTitle = _dbContext
             .Category
             .SingleOrDefault(x => x.Id == productCategory.CategoryId).CategoryTitle;


            if (productCategory == null) return NotFound();

            return View(productCategory);
        }

        public IActionResult Create(int productId)
        {
            ViewBag.ProductId = productId;

            ViewBag.Categories = _dbContext.Category
                .Select
                (
                c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoryTitle,
                }).ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int categoryId, int productId) 
        {
            if (categoryId == 0) return NotFound();

            var productCategory = _dbContext.ProductCategory.FirstOrDefault(pc => pc.Id == categoryId);

            if (productCategory == null) return NotFound();

            ViewBag.ProductId = productId;

            ViewBag.Categories = _dbContext.Category.Select
                (
                 c => new SelectListItem()
                 {
                     Value = c.Id.ToString(),
                     Text = c. CategoryTitle
                 }
                ).ToList();

            ViewBag.Products = _dbContext.Product.Select
                (
                    p => new SelectListItem()
                    {
                        Value = p.Id.ToString(),
                        Text = p.ProductTitle
                    }
                ).ToList();

            return View(productCategory);
        }

        [HttpPost]
        public IActionResult Edit(ProductCategory productCategory)
        {
            if (productCategory == null || productCategory.Id == 0) return NotFound();

            if (ModelState.IsValid)
            {
                _dbContext.ProductCategory.Update(productCategory);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index), new { productId = productCategory.ProductId });
            }

            return View(productCategory);
        }

        [HttpGet]
        public IActionResult Delete(int id) 
        {
            if (id == 0) return NotFound();

            var productCategory = _dbContext.ProductCategory.FirstOrDefault(p => p.Id == id);

            if (productCategory == null) return NotFound();


            return View(productCategory);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id) 
        {
            if (id == 0) return NotFound();
            var productCategory = _dbContext.ProductCategory.FirstOrDefault(p => p.Id == id);

            if (productCategory == null) return NotFound();

            _dbContext.ProductCategory.Remove(productCategory);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index), new { productId = productCategory.ProductId });
        }
    }
}
