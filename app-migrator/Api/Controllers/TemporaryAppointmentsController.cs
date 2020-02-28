using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> AddAppointment([FromBody] NhsNumbersModel nhsNumbers)
        {
            var result = await _orchestrator.GetFutureAppointments(nhsNumbers.NhsNumbers);
            
            return new JsonResult(result);
        }
    }

    public class NhsNumbersModel
    {
        public List<long> NhsNumbers { get; set; }
    }
}