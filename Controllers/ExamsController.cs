using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolProject.Models.Classes;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Controllers
{
    public class ExamsController : Controller
    {
        private readonly IExamService _service;
        private readonly ILessonService _lessonService;

        public ExamsController(IExamService service, ILessonService lessonService)
        {
            _service = service;
            _lessonService = lessonService;
        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            TempData["ActivePage"] = "5";

            return View(await _service.GetAllAsync());
        }

        // GET: Exams/Create
        public async Task<IActionResult> Create()
        {
            await SetViewBagData();

            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExamStartDate,ExamEndDate,IsCanceled,LessonId")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(exam);

                return RedirectToAction(nameof(Index));
            }

            return View(exam);
        }

        private async Task SetViewBagData()
        {
            ViewBag.Lessons = new SelectList(await _lessonService.GetAllAsync(), "Id", "ExamAddDropdownName");
        }

        // GET: Exams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Exam? exam = await _service.GetByIdAsync(id.GetValueOrDefault());

            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Exam? exam = await _service.GetByIdAsync(id);
            if (exam != null)
            {
                await _service.DeleteAsync(exam);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Exams/Cancel/5
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Exam? exam = await _service.GetByIdAsync(id.GetValueOrDefault());
            if (exam == null)
            {
                return NotFound();
            }

            exam.IsCanceled = true;

            await _service.UpdateAsync(exam);

            return RedirectToAction(nameof(Index));
        }
    }
}
