using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Model;
using ReservationSystem.Service;

namespace ReservationSystem.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController(IReservationService reservationService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationDto dto)
        {
            var result = await reservationService.CreateAsync(dto.Email, dto.Phone, dto.Tickets, HttpContext);
            return Ok(result);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            var r = await reservationService.GetByCodeAsync(code);
            if (r == null) return NotFound();

            return Ok(r);
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            var ok = await reservationService.DeleteAsync(code, HttpContext);
            return ok ? Ok() : NotFound();
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, [FromBody] CreateReservationDto dto)
        {
            var result = await reservationService.UpdateAsync(code, dto.Email, dto.Phone, dto.Tickets, HttpContext);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
