using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Http.Testing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GPConnectAdaptor;
using GPConnectAdaptor.Models;
using GPConnectAdaptor.Models.AddAppointment;
using Microsoft.AspNetCore.Server.HttpSys;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {

    private readonly ILogger<AppointmentsController> _logger;
    private readonly IOrchestrator _orchestrator;

        public AppointmentsController(ILogger<AppointmentsController> logger, IOrchestrator orchestrator)
        {
            _logger = logger;
            _orchestrator = orchestrator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] TempAddAppointmentRequest request)
        {
            var slotInfo = _orchestrator.GetSlotInfo(request);

            var appointment = await _orchestrator.AddAppointment(await slotInfo);
            return new JsonResult(appointment);
        }
    }
}
