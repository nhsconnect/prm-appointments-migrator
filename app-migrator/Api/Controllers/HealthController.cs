using System;
using System.Threading.Tasks;
using GPConnectAdaptor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {

        private readonly ILogger<BookAppointmentsController> _logger;

        public HealthController(ILogger<BookAppointmentsController> logger, IOrchestrator orchestrator)
        {
            _logger = logger;
        }

        [HttpGet]
        public string[] GetHealth()
        {
            return new string[]
            {
                "http://" + Environment.GetEnvironmentVariable("demonstrator2") + ":" +
                Environment.GetEnvironmentVariable("demonstratorport2") + "/gpconnect-demonstrator/v1/fhir",
                "http://" + Environment.GetEnvironmentVariable("demonstrator1") + ":" +
                Environment.GetEnvironmentVariable("demonstratorport1")+"/gpconnect-demonstrator/v1/fhir"
            };
        }
    }
}