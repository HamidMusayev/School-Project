using Microsoft.AspNetCore.Mvc;
using SchoolProject.Models.Classes;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Controllers
{
    public class ClassesController : Controller
    {
        private readonly IClassService _service;

        public ClassesController(IClassService service)
        {
            _service = service;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            TempData["ActivePage"] = "3";

            return View(await _service.GetAllAsync());
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Class? @class = await _service.GetByIdAsync(id.GetValueOrDefault());

            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Passive")] Class @class)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(@class);

                return RedirectToAction(nameof(Index));
            }

            return View(@class);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Class? @class = await _service.GetByIdAsync(id.GetValueOrDefault());
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Passive")] Class @class)
        {
            if (id != @class.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(@class);

                return RedirectToAction(nameof(Index));
            }

            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Class? @class = await _service.GetByIdAsync(id.GetValueOrDefault());

            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Class? @class = await _service.GetByIdAsync(id);

            if (@class != null)
            {
                await _service.DeleteAsync(@class);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
