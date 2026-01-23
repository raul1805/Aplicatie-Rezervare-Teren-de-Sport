using Microsoft.EntityFrameworkCore;
using SportsReservationApp.Data;

namespace SportsReservationApp.Services
{
    public class ReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsSlotAvailable(int fieldId, DateTime date, int hour)
        {
            // Requirement: "Eliminarea suprapunerilor"
            // Check if any reservation exists for the same field, date, and hour.
            // Note: Date should be just the Date part (time 00:00:00), ensuring we compare days correctly.
            
            var hasConflict = await _context.Reservations
                .AnyAsync(r => r.FieldId == fieldId 
                               && r.Date.Date == date.Date 
                               && r.StartHour == hour);

            return !hasConflict;
        }
    }
}
