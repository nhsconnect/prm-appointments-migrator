using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.AddAppointment;
using GPConnectAdaptor.Models.ReadAppointments;
using GPConnectAdaptor.Models.Slot;
using GPConnectAdaptor.Patient;
using GPConnectAdaptor.ReadAppointments;
using GPConnectAdaptor.Slots;
using Newtonsoft.Json;

namespace GPConnectAdaptor
{
    public class MigrationOrchestrator : IOrchestrator
    {
        private readonly Slots.ISlotClient _slotClient;
        private readonly IAddAppointmentClient _addAppointmentClient;
        private readonly IPatientLookup _patientLookup;
        private readonly IReadAppointmentsClient _readAppointmentsClient;

        public MigrationOrchestrator(Slots.ISlotClient slotClient, IAddAppointmentClient addAppointmentClient, IPatientLookup patientLookup, IReadAppointmentsClient readAppointmentsClient)
        {
            _slotClient = slotClient;
            _addAppointmentClient = addAppointmentClient;
            _patientLookup = patientLookup;
            _readAppointmentsClient = readAppointmentsClient;
        }
        
        public async Task<AddAppointmentResponse> AddAppointment(AddAppointmentCriteria criteria, SourceTarget sourceTarget = SourceTarget.Target)
        {
            try
            {
                return await _addAppointmentClient.AddAppointment(
                    criteria.SlotReference,
                    criteria.PatientId,
                    criteria.LocationId,
                    criteria.Start, 
                    criteria.End,
                    sourceTarget);
            }
            catch (Exception e)
            {
                throw new Exception($"Got Slots, but failed to book appointment. Returned with '{e.Message}'");
            }
            
        }
        
        public async Task<AddAppointmentCriteria> GetSlotInfo(BookAppointmentModel model, SourceTarget sourceTarget = SourceTarget.Target)
        {
            SlotResponse slots;
            Resource slot;

            try
            {
                slots =  await _slotClient.GetSlots(model.Start, model.End, sourceTarget);
                slot = FindSlot(model, slots);
            }
            catch (ArgumentNullException e)
            { 
                throw new Exception("No Slots found for this time");
            }

            var scheduleId = slot.schedule.reference.Substring(9); 
            var locationId = GetLocationId(slots, scheduleId);

            return new AddAppointmentCriteria()
            {
                PatientId = model.PatientId,
                LocationId = locationId,
                SlotReference = slot.id,
                Start = slot.start ?? new DateTime(),
                End = slot.end ?? new DateTime()
            };
        }
        
        public async Task<List<Appointment>> GetFutureAppointments(List<long> nhsNumbers)
        {
            var appointments = new List<Appointment>();
            await _patientLookup.Initialize(nhsNumbers);
            var patientIds = _patientLookup.GetPatientIds();

            foreach (var patientId in patientIds)
            {
                var appointmentsToAdd = await _readAppointmentsClient.GetFutureAppointments(patientId);
                if (appointmentsToAdd != null && appointmentsToAdd.Count > 0)
                {
                    appointments.AddRange(appointmentsToAdd);
                }
            }

            return appointments;
        }

        private static string GetLocationId(SlotResponse slots, string scheduleId)
        {
            var locationId = slots.entry.Select(e => e.resource)
                .Where(r => r.resourceType == "Schedule")
                .First(s => s.id == scheduleId)
                .actor.First(a => a.reference.StartsWith("Location/")).reference;
            return locationId;
        }

        private static Resource FindSlot(BookAppointmentModel model, SlotResponse slots)
        {
            return slots.entry
                .Select(e => e.resource)
                .Where(r => r.resourceType == "Slot")
                .First(s =>
                    s.start >= model.Start.Subtract(new TimeSpan(0, 0, 1)) &&
                    s.end <= model.End.AddSeconds(1));
        }
    }

    public class AddAppointmentCriteria
    {
        public string SlotReference { get; set; }
        public string PatientId { get; set; }
        public string LocationId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
    

    public class BookAppointmentModel
    {
        public string PatientId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        
        public string LocationId { get; set; }
        
        public string PractitionerId { get; set; }
    }
}