namespace ReservationSystem.Entity
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Code { get; set; } = Guid.NewGuid().ToString("N");

        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int TicketCount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
