using System;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.AddAppointment;
using GPConnectAdaptor.Patient;
using GPConnectAdaptor.Slots;
using Newtonsoft.Json;

namespace GPConnectAdaptor.AddAppointment
{
    public class AddAppointmentClient : IAddAppointmentClient
    {
        private readonly IAddAppointmentRequestBuilder _addAppointmentRequestBuilder;
        private readonly IAddAppointmentHttpClientWrapper _httpClientWrapper;
        private IAppointmentBookedModelMapper _appointmentBookedModelMapper;

        public AddAppointmentClient(IJwtTokenGenerator tokenGenerator,
            IAddAppointmentRequestBuilder addAppointmentRequestBuilder,
            IAddAppointmentHttpClientWrapper httpClientWrapper,
            IAppointmentBookedModelMapper appointmentBookedModelMapper)
        {
            _addAppointmentRequestBuilder = addAppointmentRequestBuilder;
            _httpClientWrapper = httpClientWrapper;
            _appointmentBookedModelMapper = appointmentBookedModelMapper;
        }
        
        public async Task<AppointmentBookedModel> AddAppointment(string slotRef,
            string patientRef,
            string locationRef,
            DateTime start,
            DateTime end, 
            SourceTarget sourceTarget,
            IPatientLookup patientLookup)
        {
            var request = _addAppointmentRequestBuilder.Build(slotRef, patientRef, locationRef, start, end);
            var appointmentRequestBody = JsonConvert.SerializeObject(request);
            var appointmentResponseBody = await _httpClientWrapper.PostAsync(appointmentRequestBody, sourceTarget);
            
            var appointment = _appointmentBookedModelMapper.Map(appointmentResponseBody, patientLookup);

            return appointment;
        }
    }
}