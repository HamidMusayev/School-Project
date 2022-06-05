using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolProject.Models.Classes;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _service;
        private readonly IClassService _classService;

        public StudentsController(IStudentService service, IClassService classService)
        {
            _service = service;
            _classService = classService;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            TempData["ActivePage"] = "2";

            return View(await _service.GetAllAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User? user = await _service.GetByIdAsync(id.GetValueOrDefault());

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Students/Create
        public async Task<IActionResult> CreateAsync()
        {
            await SetViewBagData();

            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Number,Email,ClassId")] User user)
        {
            user.Type = Models.Enums.UserType.Tələbə;

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(user);

                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            await SetViewBagData();

            if (id == null)
            {
                return NotFound();
            }

            User? user = await _service.GetByIdAsync(id.GetValueOrDefault());

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        private async Task SetViewBagData()
        {
            ViewBag.Classes = new SelectList(await _classService.GetAllAsync(), "Id", "Name");
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassId,Name,Surname,Number,Email,Passive")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            user.Type = Models.Enums.UserType.Tələbə;

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(user);

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User? user = await _service.GetByIdAsync(id.GetValueOrDefault());

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            User? user = await _service.GetByIdAsync(id);

            if (user != null)
            {
                await _service.DeleteAsync(user);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
