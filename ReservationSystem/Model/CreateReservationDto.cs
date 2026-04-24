using ReservationSystem.Validation;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Model
{
    public class CreateReservationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [CzechPhone]
        public string Phone { get; set; } = null!;

        [Range(1, 10)]
        public int Tickets { get; set; }
    }
}
