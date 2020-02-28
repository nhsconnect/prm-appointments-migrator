using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.ReadAppointments;

namespace GPConnectAdaptor.ReadAppointments
{
    public class ReadAppointmentsClient : IReadAppointmentsClient
    {
        private readonly IReadAppointmentsHttpClientWrapper _httpClientWrapper;
        private readonly IAppointmentsResponseMapper _mapper;
        

        public ReadAppointmentsClient(IReadAppointmentsHttpClientWrapper httpClientWrapper, IAppointmentsResponseMapper mapper)
        {
            _httpClientWrapper = httpClientWrapper;
            _mapper = mapper;
        }

        public async Task<List<Appointment>> GetFutureAppointments(int patientId)
        {
            var start = DateTime.Now;
            var end = start.AddDays(60);
            var response = await _httpClientWrapper.GetAsync(start, end, patientId);
            return _mapper.Map(response);
        }
    }
}