using System;
using System.Threading.Tasks;
using GPConnectAdaptor;
using GPConnectAdaptor.Slots;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InitialiseDummyAppointmentsController : ControllerBase
    {
        private readonly ILogger<BookAppointmentsController> _logger;
        private readonly IOrchestrator _orchestrator;

        public InitialiseDummyAppointmentsController(ILogger<BookAppointmentsController> logger, IOrchestrator orchestrator)
        {
            _logger = logger;
            _orchestrator = orchestrator;
        }

        [HttpPost]
        public async Task<IActionResult> AddDummyAppointment([FromBody] BookAppointmentModel bookAppointment)
        {
            var slot = _orchestrator.GetSlotInfo(bookAppointment, SourceTarget.Source);

            var appointment = await _orchestrator.AddAppointment(await slot, bookAppointment.PatientId, SourceTarget.Source);
            return new JsonResult(appointment);
        }
    }
}