﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.AddAppointment;
using GPConnectAdaptor.Models.ReadAppointments;
using GPConnectAdaptor.Models.Slot;
using GPConnectAdaptor.Patient;
using GPConnectAdaptor.ReadAppointments;

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

        /// <summary>
        /// Add Appointment. Also temporary.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<AddAppointmentResponse> AddAppointment(AddAppointmentCriteria criteria)
        {
            try
            {
                return await _addAppointmentClient.AddAppointment(
                    criteria.SlotReference,
                    criteria.PatientId,
                    criteria.LocationId,
                    criteria.Start, criteria.End);
            }
            catch (Exception e)
            {
                throw new Exception($"Got Slots, but failed to book appointment. Returned with '{e.Message}'");
            }
            
        }
        
        
        /// <summary>
        /// This is temporary and obsolete. Will be removed.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AddAppointmentCriteria> GetSlotInfo(TempAddAppointmentRequest request)
        {
            SlotResponse slots;
            Resource slot;

            try
            {
                slots =  await _slotClient.GetSlots(request.Start, request.End);
                slot = FindSlot(request, slots);
            }
            catch (ArgumentNullException e)
            { 
                throw new Exception("No Slots found for this time");
            }

            var scheduleId = slot.schedule.reference.Substring(9); //actual id starts at 9th char because of weird contract
            var locationId = GetLocationId(slots, scheduleId);

            return new AddAppointmentCriteria()
            {
                PatientId = request.PatientId,
                LocationId = locationId,
                SlotReference = slot.id,
                Start = slot.start ?? new DateTime(),
                End = slot.end ?? new DateTime()
            };
        }

        /// <summary>
        /// temporary method to test
        /// </summary>
        /// <param name="nhsNumbers"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Appointment>> GetFutureAppointments(List<long> nhsNumbers)
        {
            var appointments = new List<Appointment>();
            await _patientLookup.Initialize(nhsNumbers);
            var patientIds = _patientLookup.GetPatientIds();

            foreach (var patientId in patientIds)
            {
                appointments.AddRange(await _readAppointmentsClient.GetFutureAppointments(patientId));
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

        private static Resource FindSlot(TempAddAppointmentRequest request, SlotResponse slots)
        {
            return slots.entry
                .Select(e => e.resource)
                .Where(r => r.resourceType == "Slot")
                .First(s =>
                    s.start >= request.Start.Subtract(new TimeSpan(0, 0, 1)) &&
                    s.end <= request.End.AddSeconds(1));
        }
    }
    
    /// <summary>
    /// Temporary, will be obsolete
    /// </summary>
    public class AddAppointmentCriteria
    {
        public string SlotReference { get; set; }
        public string PatientId { get; set; }
        public string LocationId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
    
    /// <summary>
    /// Temporary and will be obsolete
    /// </summary>
    public class TempAddAppointmentRequest
    {
        public string PatientId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}