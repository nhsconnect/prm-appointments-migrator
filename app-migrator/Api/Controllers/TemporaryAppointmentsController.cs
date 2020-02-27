using System.Collections.Generic;
using System.Threading.Tasks;
using GPConnectAdaptor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemporaryAppointmentsController
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly IOrchestrator _orchestrator;

        public TemporaryAppointmentsController(ILogger<AppointmentsController> logger, IOrchestrator orchestrator)
        {
            _logger = logger;
            _orchestrator = orchestrator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] List<long> nhsNumbers)
        {
            var result = await _orchestrator.GetFutureAppointments(nhsNumbers);
            
            return new JsonResult(result);
        }
    }
}