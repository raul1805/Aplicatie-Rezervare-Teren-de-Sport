using System.ComponentModel.DataAnnotations;

namespace SportsReservationApp.Models
{
    public class SportField
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nume Teren")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Sport")]
        public string Type { get; set; } = string.Empty; // e.g. Fotbal, Tenis, Baschet

        [Display(Name = "Indoor/Outdoor")]
        public bool IsIndoor { get; set; }

        [Range(1, 100)]
        [Display(Name = "Capacitate (Totala)")]
        public int Capacity { get; set; }

        [Display(Name = "Descriere")]
        public string? Description { get; set; }
    }
}
