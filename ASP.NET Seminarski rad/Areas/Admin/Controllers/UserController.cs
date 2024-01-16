using ASP.NET_Seminarski_rad.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Seminarski_rad.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        ApplicationDbContext _dbContext;
        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var users = _dbContext.Users.ToList();
            return View(users);
        }

        public IActionResult Update(string id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            
            if (user == null) 
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Update(string Id, string FirstName, string LastName) 
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == Id);

            user.FirstName = FirstName;
            user.LastName = LastName;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
