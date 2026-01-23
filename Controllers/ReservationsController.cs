using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsReservationApp.Data;
using SportsReservationApp.Models;
using SportsReservationApp.Services;

namespace SportsReservationApp.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ReservationService _reservationService;

        public ReservationsController(ApplicationDbContext context, ReservationService reservationService)
        {
            _context = context;
            _reservationService = reservationService;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservations.Include(r => r.Field).OrderByDescending(r => r.Date).ThenBy(r => r.StartHour);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["FieldId"] = new SelectList(_context.SportFields, "Id", "Name");
            return View();
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerName,CustomerContact,FieldId,Date,StartHour")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                // Check business rules
                if (reservation.StartHour < 8 || reservation.StartHour > 21)
                {
                    ModelState.AddModelError("StartHour", "Rezervarile se pot face doar intre orele 08:00 si 22:00.");
                }

                if (await _reservationService.IsSlotAvailable(reservation.FieldId, reservation.Date, reservation.StartHour) == false)
                {
                    ModelState.AddModelError("", "Acest interval orar este deja rezervat pentru terenul selectat.");
                }

                if (ModelState.IsValid)
                {
                    _context.Add(reservation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["FieldId"] = new SelectList(_context.SportFields, "Id", "Name", reservation.FieldId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var reservation = await _context.Reservations
                .Include(r => r.Field)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null) return NotFound();

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
