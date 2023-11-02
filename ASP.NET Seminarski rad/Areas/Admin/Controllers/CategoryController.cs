using ASP.NET_Seminarski_rad.Data;
using ASP.NET_Seminarski_rad.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Seminarski_rad.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext _dbcontext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public IActionResult Index()
        {
            var categories = _dbcontext.Category.ToList();
            return View(categories);
        }

        public IActionResult Details(int id) 
        {
            var category = _dbcontext.Category.FirstOrDefault(c => c.Id == id);

            if (id == 0 || id == null || category == null) 
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(ModelState.IsValid) 
            {
                _dbcontext.Category.Add(category);
                _dbcontext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int? id) 
        {
            var category = _dbcontext.Category.FirstOrDefault(c => c.Id == id);

            if (id == null || id == 0 || category == null) 
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if(ModelState.IsValid) 
            {
                _dbcontext.Category.Update(category);
                _dbcontext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var category = _dbcontext.Category.FirstOrDefault(c => c.Id == id);

            if (id == null || id == 0 || category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id) 
        {
            if(id == 0)
            {
                return NotFound();
            }

            var category = _dbcontext.Category.FirstOrDefault(c => c.Id == id);

            _dbcontext.Category.Remove(category);
            _dbcontext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
