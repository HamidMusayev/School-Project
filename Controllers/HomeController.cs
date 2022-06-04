using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Models;
using SchoolProject.Models.Classes;
using SchoolProject.Models.Contexts;
using System.Diagnostics;

namespace SchoolProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchoolContext _context;

        public HomeController(ILogger<HomeController> logger, SchoolContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.StudentCount = _context.Users.Count(u=>u.Type == Models.Enums.UserType.Tələbə && u.Passive == false);
            ViewBag.TeacherCount = _context.Users.Count(u => u.Type == Models.Enums.UserType.Müəllim && u.Passive == false);
            ViewBag.ClassCount = _context.Classes.Count(u => u.Passive == false);
            ViewBag.ExamCount = _context.Exams.Count(u => u.IsCanceled == false);

            User sessionUser = _context.Users.Where(u => u.Name == "Hamid").ToList()[0];

            return View(sessionUser);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}