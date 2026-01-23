using System.ComponentModel.DataAnnotations;

namespace SportsReservationApp.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nume Client")]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Contact")]
        public string CustomerContact { get; set; } = string.Empty;

        [Required]
        public int FieldId { get; set; }
        public SportField? Field { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Required]
        [Range(8, 21, ErrorMessage = "Programarile se fac intre ora 8:00 si 22:00")]
        [Display(Name = "Ora de Start")]
        public int StartHour { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
