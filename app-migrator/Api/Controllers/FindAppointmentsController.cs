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
        private readonly ILogger<BookAppointmentsController> _logger;
        private readonly IOrchestrator _orchestrator;

        public FindAppointmentsController(ILogger<BookAppointmentsController> logger, IOrchestrator orchestrator)
        {
            _logger = logger;
            _orchestrator = orchestrator;
        }

        [HttpGet]
        public async Task<IActionResult> AddAppointment()
        {
            var result = await _orchestrator.GetFutureAppointments();
            return new JsonResult(result);
        }
    }
}