using Microsoft.EntityFrameworkCore;
using ReservationSystem.DatabaseContext;
using ReservationSystem.Entity;

namespace ReservationSystem.Service
{
    public class ReservationService(AppDbContext appDbContext) : IReservationService
    {
        public async Task<Reservation> CreateAsync(string email, string phone, int tickets, HttpContext context)
        {
            if (tickets > 10)
                throw new Exception("Max 10 tickets");

            var total = await appDbContext.Reservations.SumAsync(r => r.TicketCount);

            if (total + tickets > 100)
                throw new Exception("Capacity exceeded");

            var reservation = new Reservation
            {
                Email = email,
                Phone = phone,
                TicketCount = tickets
            };

            appDbContext.Reservations.Add(reservation);

            await Log("CREATE", reservation.Id, context);

            await appDbContext.SaveChangesAsync();

            return reservation;
        }

        public async Task<Reservation?> GetByCodeAsync(string code)
        {
            return await appDbContext.Reservations.FirstOrDefaultAsync(r => r.Code == code);
        }

        public async Task<bool> DeleteAsync(string code, HttpContext context)
        {
            var r = await appDbContext.Reservations.FirstOrDefaultAsync(x => x.Code == code);
            if (r == null) return false;

            appDbContext.Reservations.Remove(r);

            await Log("DELETE", r.Id, context);

            await appDbContext.SaveChangesAsync();
            return true;
        }

        private async Task Log(string action, int? reservationId, HttpContext context)
        {
            var log = new AuditLog
            {
                Action = action,
                ReservationId = reservationId,
                IpAddress = context.Connection.RemoteIpAddress?.ToString(),
                UserAgent = context.Request.Headers["User-Agent"]
            };

            appDbContext.AuditLogs.Add(log);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<Reservation?> UpdateAsync(string code, string email, string phone, int tickets, HttpContext context)
        {
            if (tickets > 10)
                throw new Exception("Max 10 tickets");

            var reservation = await appDbContext.Reservations.FirstOrDefaultAsync(r => r.Code == code);
            if (reservation == null)
                return null;

            var total = await appDbContext.Reservations.SumAsync(r => r.TicketCount);

            var adjustedTotal = total - reservation.TicketCount + tickets;

            if (adjustedTotal > 100)
                throw new Exception("Capacity exceeded");

            reservation.Email = email;
            reservation.Phone = phone;
            reservation.TicketCount = tickets;

            await Log("UPDATE", reservation.Id, context);

            await appDbContext.SaveChangesAsync();

            return reservation;
        }
    }
}
