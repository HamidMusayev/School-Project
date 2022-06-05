using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolProject.Models.Classes;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ILessonService _service;
        private readonly IClassService _classService;
        private readonly IStudentService _studentService;

        public LessonsController(ILessonService service, IClassService classService, IStudentService studentService)
        {
            _service = service;
            _classService = classService;
            _studentService = studentService;
        }

        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            TempData["ActivePage"] = "4";


            return View(await _service.GetAllAsync());
        }

        // GET: Lessons/Create
        public async Task<IActionResult> Create()
        {
            await SetViewBagData();
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Passive,TeacherId,ClassId")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(lesson);

                return RedirectToAction(nameof(Index));
            }

            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Lesson? lesson = await _service.GetByIdAsync(id.GetValueOrDefault());

            await SetViewBagData();

            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        private async Task SetViewBagData()
        {
            ViewBag.Classes = new SelectList(await _classService.GetAllAsync(), "Id", "Name");
            ViewBag.Teachers = new SelectList(await _studentService.GetAllAsync(), "Id", "Name");
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Passive,ClassId,TeacherId")] Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(lesson);

                return RedirectToAction(nameof(Index));
            }

            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Lesson? lesson = await _service.GetByIdAsync(id.GetValueOrDefault());

            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Lesson? lesson = await _service.GetByIdAsync(id);

            if (lesson != null)
            {
                await _service.DeleteAsync(lesson);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
