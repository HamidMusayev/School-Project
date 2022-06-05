using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Models.Classes;
using SchoolProject.Models.Contexts;

namespace SchoolProject.Controllers
{
    public class LessonsController : Controller
    {
        private readonly SchoolContext _context;

        public LessonsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            TempData["ActivePage"] = "4";

            if (_context.Lessons == null)
            {
                return NotFound();
            }

            return View(await _context.Lessons.Where(l => l.Passive == false)
                .Include(l => l.Class)
                .Include(l => l.Teacher)
                .ToListAsync());
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
                _context.Add(lesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            Lesson? lesson = await _context.Lessons
                .Where(l => l.Id == id)
                .Include(l => l.Class)
                .Include(l => l.Teacher)
                .FirstOrDefaultAsync();

            await SetViewBagData();

            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        private async Task SetViewBagData()
        {
            ViewBag.Classes = new SelectList(await _context.Classes.Where(c => c.Passive == false).ToListAsync(), "Id", "Name");
            ViewBag.Teachers = new SelectList(await _context.Users.Where(c => c.Passive == false && c.Type == Models.Enums.UserType.Müəllim).ToListAsync(), "Id", "Name");
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
                try
                {
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lessons == null)
            {
                return NotFound();
            }

            Lesson? lesson = await _context.Lessons
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (_context.Lessons == null)
            {
                return Problem("Entity set 'SchoolContext.Lessons'  is null.");
            }
            Lesson? lesson = await _context.Lessons.FindAsync(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
            return (_context.Lessons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
