﻿using Microsoft.AspNetCore.Mvc;
using SchoolProject.Models;
using SchoolProject.Models.Classes;
using SchoolProject.Services.Abstract;
using System.Diagnostics;

namespace SchoolProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _service;
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;

        public HomeController(ILogger<HomeController> logger, IHomeService service, IStudentService studentService,
            IClassService classService)
        {
            _logger = logger;
            _service = service;
            _studentService = studentService;
            _classService = classService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            TempData["ActivePage"] = "1";

            int[] counts = await _service.GetCounts();

            ViewBag.StudentCount = counts.ElementAt(0);
            ViewBag.TeacherCount = counts.ElementAt(1);
            ViewBag.ClassCount = counts.ElementAt(2);
            ViewBag.ExamCount = counts.ElementAt(3);

            //SESSIONDAN OXUNACAQ, TEST UCUN SECILIB
            User? sessionUser = await _studentService.GetByIdAsync(1);

            if (sessionUser == null)
            {
                //EGER DATABASE BOSDURSA FIRST TIME TEST UCUN ELAVE ET VE SEHIFENI YENILE
                await _classService.AddAsync(new Class
                {
                    Name = "M300",
                    Passive = false,
                });
                
                await _studentService.AddAsync(new User
                {
                    Email = "hemidvmusayev@gmail.com",
                    Name = "Hamid",
                    Surname = "Musayev",
                    Number = "0508218170",
                    Type = Models.Enums.UserType.Müəllim,
                    ClassId = 1,
                    Passive = false,
                });

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