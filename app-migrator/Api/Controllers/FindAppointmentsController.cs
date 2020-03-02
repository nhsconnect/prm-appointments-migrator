using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPConnectAdaptor;
using GPConnectAdaptor.Models.ReadAppointments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FindAppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly IOrchestrator _orchestrator;
        private readonly List<long> _nhsNumbers;

        public FindAppointmentsController(ILogger<AppointmentsController> logger, IOrchestrator orchestrator)
        {
            _logger = logger;
            _orchestrator = orchestrator;
            _nhsNumbers = GetHardcodedNhsNumbers();
        }

        [HttpGet]
        public async Task<IActionResult> AddAppointment()
        {
            var result = await _orchestrator.GetFutureAppointments(_nhsNumbers);
            return new JsonResult(result);
        }
        
        private List<long> GetHardcodedNhsNumbers()
        {
            return new List<long>()
            {
                9658218865,
                9658218873,
                9658218881,
                9658218903,
                9658218989,
                9658218997,
                9658219004,
                9658219012
            };
        }
    }
}