using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPConnectAdaptor.AddAppointment;
using GPConnectAdaptor.Models.AddAppointment;
using GPConnectAdaptor.Models.ReadAppointments;
using GPConnectAdaptor.Models.Slot;
using GPConnectAdaptor.Patient;
using GPConnectAdaptor.Practitioner;
using GPConnectAdaptor.ReadAppointments;
using GPConnectAdaptor.Slots;
using Newtonsoft.Json;

namespace GPConnectAdaptor
{
    public class MigrationOrchestrator : IOrchestrator
    {
        private readonly IAddAppointmentClient _addAppointmentClient;
        private readonly IPatientLookup _patientLookup;
        private readonly IPractitionerLookup _practitionerLookup;
        private readonly IReadAppointmentsClient _readAppointmentsClient;
        private readonly ISlotRetriever _slotRetriever;

        public MigrationOrchestrator(IAddAppointmentClient addAppointmentClient,
            IPatientLookup patientLookup,
            IReadAppointmentsClient readAppointmentsClient,
            ISlotRetriever slotRetriever, 
            IPractitionerLookup practitionerLookup)
        {
            _addAppointmentClient = addAppointmentClient;
            _patientLookup = patientLookup;
            _readAppointmentsClient = readAppointmentsClient;
            _slotRetriever = slotRetriever;
            _practitionerLookup = practitionerLookup;
        }
        
        public async Task<AppointmentBookedModel> AddAppointment(
            SlotModel slot,
            string patientId,
            SourceTarget sourceTarget = SourceTarget.Target)
        {
            await InitialiseLookups();

            try
            {
                return await _addAppointmentClient.AddAppointment(slot, patientId, sourceTarget, _patientLookup, _practitionerLookup);
            }
            catch (Exception e)
            {
                throw new Exception($"Got Slots, but failed to book appointment. Returned with '{e.Message}'");
            }
        }

        public async Task<SlotModel> GetSlotInfo(Appointment sourceAppointment, SourceTarget sourceTarget = SourceTarget.Target)
        {
            return await _slotRetriever.RetrieveSlotFromTarget(sourceAppointment, sourceTarget);
        }
        
        public async Task<SlotModel> GetSlotInfo(BookAppointmentModel model, SourceTarget sourceTarget = SourceTarget.Target)
        {
            return await _slotRetriever.RetrieveSlotFromSource(model, SourceTarget.Source);
        }
        
        public async Task<List<Appointment>> GetFutureAppointments()
        {
            await InitialiseLookups();

            var appointments = new List<Appointment>();
            var patientIds = _patientLookup.GetPatientIds();

            foreach (var patientId in patientIds)
            {
                var appointmentsToAdd = await _readAppointmentsClient.GetFutureAppointments(patientId, _patientLookup, _practitionerLookup);
                if (appointmentsToAdd != null && appointmentsToAdd.Count > 0)
                {
                    appointments.AddRange(appointmentsToAdd);
                }
            }

            return appointments;
        }
        
        private async Task InitialiseLookups()
        {
            if (!_patientLookup.IsInitialized())
            {
                await _patientLookup.Initialize();
            }

            if (!_practitionerLookup.IsInitialized())
            {
                await _practitionerLookup.Initialize();
            }
        }
    }
}