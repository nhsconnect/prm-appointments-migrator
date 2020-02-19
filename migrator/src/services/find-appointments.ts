import axios, { AxiosError } from 'axios';
import { commonHeaders, domain1 } from './common';
import { buildRequest } from './jwt';

export const findAppointments = async () => {
    const interactionId = 'urn:nhs:names:services:gpconnect:fhir:rest:search:patient_appointments-1';

    const headers = {
        ...commonHeaders,
        'Ssp-InteractionID': interactionId,
        'Authorization': `Bearer ${buildRequest()}`
    };

    const start = '2020-02-18';
    const end = '2020-02-22';
    
    const url = `${domain1}/Patient/2/Appointment?start=ge${start}&start=le${end}`;

    const res = await axios.get(url, { headers }).catch((error: AxiosError) => {
        const { response } = error;
        return response || { data: error };
    });

    return transform(res.data);
};

const transform = (payload) => {
    const { entry: appointments } = payload;
    const lift = appointments
        .map(appointment => appointment.resource)
        .sort((a, b) => Number(a.id) - Number(b.id));

    const flatten = lift.map(({ id, description, start, end, minutesDuration, slot, participant }) => {
        return {
            patientId: participant[0].actor.reference,
            slot: slot[0].reference,
            location: participant[1].actor.reference,
            practitioner: participant[2].actor.reference,
            start,
            end,
            minutesDuration,
            description
        }
    })
    return flatten;
};
