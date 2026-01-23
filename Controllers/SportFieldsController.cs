using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsReservationApp.Data;
using SportsReservationApp.Models;

namespace SportsReservationApp.Controllers
{
    public class SportFieldsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SportFieldsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SportFields
        public async Task<IActionResult> Index()
        {
            return View(await _context.SportFields.ToListAsync());
        }

        // GET: SportFields/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var sportField = await _context.SportFields
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sportField == null) return NotFound();

            return View(sportField);
        }

        // GET: SportFields/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SportFields/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,IsIndoor,Capacity,Description")] SportField sportField)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sportField);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sportField);
        }

        // GET: SportFields/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sportField = await _context.SportFields.FindAsync(id);
            if (sportField == null) return NotFound();
            return View(sportField);
        }

        // POST: SportFields/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,IsIndoor,Capacity,Description")] SportField sportField)
        {
            if (id != sportField.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sportField);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportFieldExists(sportField.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sportField);
        }

        // GET: SportFields/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var sportField = await _context.SportFields
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sportField == null) return NotFound();

            return View(sportField);
        }

        // POST: SportFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sportField = await _context.SportFields.FindAsync(id);
            if (sportField != null)
            {
                _context.SportFields.Remove(sportField);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SportFieldExists(int id)
        {
            return _context.SportFields.Any(e => e.Id == id);
        }
    }
}
