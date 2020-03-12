﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPConnectAdaptor.AddAppointment;
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
        private readonly IAddAppointmentClient _addAppointmentClient;
        private readonly IPatientLookup _patientLookup;
        private readonly IReadAppointmentsClient _readAppointmentsClient;
        private readonly ISlotRetriever _slotRetriever;

        public MigrationOrchestrator(IAddAppointmentClient addAppointmentClient,
            IPatientLookup patientLookup,
            IReadAppointmentsClient readAppointmentsClient,
            ISlotRetriever slotRetriever)
        {
            _addAppointmentClient = addAppointmentClient;
            _patientLookup = patientLookup;
            _readAppointmentsClient = readAppointmentsClient;
            _slotRetriever = slotRetriever;
        }
        
        public async Task<AppointmentBookedModel> AddAppointment(SlotModel slot,
            string patientId,
            SourceTarget sourceTarget = SourceTarget.Target)
        {
            await InitialisePatientLookup();

            try
            {
                return await _addAppointmentClient.AddAppointment(slot, patientId, sourceTarget, _patientLookup);
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
            await InitialisePatientLookup();
            
            var appointments = new List<Appointment>();
            var patientIds = _patientLookup.GetPatientIds();

            foreach (var patientId in patientIds)
            {
                var appointmentsToAdd = await _readAppointmentsClient.GetFutureAppointments(patientId, _patientLookup);
                if (appointmentsToAdd != null && appointmentsToAdd.Count > 0)
                {
                    appointments.AddRange(appointmentsToAdd);
                }
            }

            return appointments;
        }
        
        private async Task InitialisePatientLookup()
        {
            if (!_patientLookup.IsInitialized())
            {
                await _patientLookup.Initialize();
            }
        }
    }
}