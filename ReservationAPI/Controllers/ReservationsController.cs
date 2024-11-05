using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationAPI.Entities.RequestModel;

namespace ReservatıonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationsController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("make-reservation")]
        public IActionResult MakeReservation([FromBody] ReservationRequestModel request)
        {
            var result = _reservationService.MakeReservation(request);

            
            if (!result.CanReserve)
            {
                return Ok(result); 
            }

            return Ok(result);
        }

    }
}
