using AuthApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AuthApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Show user tabler
        [HttpGet]
        public IActionResult Database()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
    }
}
