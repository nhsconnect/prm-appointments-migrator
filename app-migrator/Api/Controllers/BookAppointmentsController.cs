using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GPConnectAdaptor;
using GPConnectAdaptor.Models.AddAppointment;
using GPConnectAdaptor.Models.ReadAppointments;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookAppointmentsController : ControllerBase
    {

    private readonly ILogger<BookAppointmentsController> _logger;
    private readonly IOrchestrator _orchestrator;

        public BookAppointmentsController(ILogger<BookAppointmentsController> logger, IOrchestrator orchestrator)
        {
            _logger = logger;
            _orchestrator = orchestrator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] List<Appointment> sourceAppointments)
        {
            var bookedAppointments = new List<AppointmentBookedModel>();

            foreach (var sourceAppointment in sourceAppointments)
            {
                try
                {
                    var addAppointmentCriteria = _orchestrator.GetSlotInfo(sourceAppointment);

                    var bookedAppointment = await _orchestrator.AddAppointment(await addAppointmentCriteria);
                
                    bookedAppointments.Add(bookedAppointment);

                }
                catch (Exception e)
                {
                    bookedAppointments.Add(new AppointmentBookedModel()
                    {
                        PatientId = sourceAppointment.PatientId,
                        Patient = sourceAppointment.Patient,
                        Success = false,
                        Error = $"Unable to book. Returned with error : '{e.Message}'",
                        Start = sourceAppointment.Start,
                        End = sourceAppointment.End
                    });
                }
                
            }
            
            return new JsonResult(bookedAppointments);
        }
    }
}
