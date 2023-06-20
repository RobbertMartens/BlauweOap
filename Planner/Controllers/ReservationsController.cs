using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Planner.Handlers;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Planner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationsController : Controller
    {
        private readonly ILogger<ReservationsController> _logger;
        private readonly IMediator _mediator;

        public ReservationsController(ILogger<ReservationsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([Required] int userId)
        {
            var result = await _mediator.Send(new GetReservationRequest { PartyPeopleId = userId });

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([Required] PostReservationRequest request)
        {
            var result = await _mediator.Send(request);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([Required] PutReservationRequest request)
        {
            var result = await _mediator.Send(request);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([Required] DeleteReservationRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsCompleted)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
