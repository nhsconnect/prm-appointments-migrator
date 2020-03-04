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
                Environment.GetEnvironmentVariable("demonstrator1"),
                Environment.GetEnvironmentVariable("demonstrator2")
            };
        }
    }
}