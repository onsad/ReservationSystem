namespace ReservationSystem.Entity
{
    public class AuditLog
    {
        public int Id { get; set; }

        public int? ReservationId { get; set; }
        public string Action { get; set; } = null!;

        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
