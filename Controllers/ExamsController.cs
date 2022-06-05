using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Models.Classes;
using SchoolProject.Models.Contexts;

namespace SchoolProject.Controllers
{
    public class ExamsController : Controller
    {
        private readonly SchoolContext _context;

        public ExamsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            return _context.Exams != null
                ? View(await _context.Exams
                .Include(e => e.Lesson)
                .Include(e => e.Lesson!.Class)
                .Include(e => e.Lesson!.Teacher).ToListAsync())
                : Problem("Entity set 'SchoolContext.Exams'  is null.");
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
        public async Task<IActionResult> Create([Bind("Id,ExamStartDate,ExamEndDate,IsCanceled,Lesson")] Exam exam)
        {
            exam.Lesson = await _context.Lessons.FindAsync(int.Parse(Request.Form["Lesson"].First()));

            if (ModelState.IsValid)
            {
                _context.Add(exam);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(exam);
        }

        private async Task SetViewBagData()
        {
            ViewBag.Lessons = new SelectList(await _context.Lessons
                .Where(c => c.Passive == false)
                .Include(l => l.Class)
                .OrderBy(l => l.Class!.Id)
                .ToListAsync(), "Id", "ExamAddDropdownName");
        }

        // GET: Exams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .FirstOrDefaultAsync(m => m.Id == id);

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
            if (_context.Exams == null)
            {
                return Problem("Entity set 'SchoolContext.Exams'  is null.");
            }
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
            return (_context.Exams?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Exams/Cancel/5
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            exam.IsCanceled = true;

            _context.Exams.Update(exam);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
