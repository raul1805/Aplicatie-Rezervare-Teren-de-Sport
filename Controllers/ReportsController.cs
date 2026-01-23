using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsReservationApp.Data;
using SportsReservationApp.Models;

namespace SportsReservationApp.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var totalReservations = await _context.Reservations.CountAsync();
            
            var popularFields = await _context.Reservations
                .GroupBy(r => r.FieldId)
                .Select(g => new
                {
                    FieldId = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            var fieldNames = new Dictionary<int, string>();
            foreach (var item in popularFields)
            {
                var field = await _context.SportFields.FindAsync(item.FieldId);
                if (field != null)
                {
                    fieldNames[item.FieldId] = field.Name + " (" + field.Type + ")";
                }
            }

            ViewBag.TotalReservations = totalReservations;
            ViewBag.PopularFields = popularFields;
            ViewBag.FieldNames = fieldNames;

            return View();
        }
    }
}
