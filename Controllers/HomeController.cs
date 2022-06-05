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

        public async Task<IActionResult> IndexAsync()
        {
            TempData["ActivePage"] = "1";

            ViewBag.StudentCount = await _context.Users.CountAsync(u => u.Type == Models.Enums.UserType.Tələbə && u.Passive == false);
            ViewBag.TeacherCount = await _context.Users.CountAsync(u => u.Type == Models.Enums.UserType.Müəllim && u.Passive == false);
            ViewBag.ClassCount = await _context.Classes.CountAsync(u => u.Passive == false);
            ViewBag.ExamCount = await _context.Exams.CountAsync(u => u.IsCanceled == false);

            //SESSIONDAN OXUNACAQ, TEST UCUN SECILIB
            User? sessionUser = await _context.Users.SingleOrDefaultAsync(u => u.Name == "Hamid");

            if (sessionUser == null || _context.Users == null)
            {
                //EGER BD BOSDURSA FIRST TIME TEST UCUN ELAVE ET VE SEHIFENI YENILE
                await _context.AddAsync(new User
                {
                    Email = "hemidvmusayev@gmail.com",
                    Name = "Hamid",
                    Surname = "Musayev",
                    Number = "0508218170",
                    Type = Models.Enums.UserType.Müəllim,
                    Passive = false,
                });
                await _context.SaveChangesAsync();

                return NotFound();
            }

            return View(sessionUser);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}