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
        private readonly ILogger<AppointmentsController> _logger;
        private readonly IOrchestrator _orchestrator;

        public InitialiseDummyAppointmentsController(ILogger<AppointmentsController> logger, IOrchestrator orchestrator)
        {
            _logger = logger;
            _orchestrator = orchestrator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] BookAppointmentModel model)
        {
            var slotInfo = _orchestrator.GetSlotInfo(model, SourceTarget.Source);

            var appointment = await _orchestrator.AddAppointment(await slotInfo, SourceTarget.Source);
            return new JsonResult(appointment);
        }
    }
}