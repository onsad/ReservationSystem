using ReservationSystem.Entity;

namespace ReservationSystem.Service
{
    public interface IReservationService
    {
        Task<Reservation> CreateAsync(string email, string phone, int tickets, HttpContext context);
        Task<Reservation?> GetByCodeAsync(string code);
        Task<bool> DeleteAsync(string code, HttpContext context);
        Task<Reservation?> UpdateAsync(string code, string email, string phone, int tickets, HttpContext context);
    }
}
