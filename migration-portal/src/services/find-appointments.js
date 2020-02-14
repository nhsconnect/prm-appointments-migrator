import { api, domainOptions } from '../config/features';
import { commonHeaders, domain } from './common';
import { buildRequest } from './jwt';
import { gpconnectTransformer } from './transformers/gpconnect';
import { migratorTransformer } from './transformers/migrator';
import { mockFindAppointments } from './mock/appointments';
import { superfetch } from './superfetch';

export const findAppointments = async () => {
    const map = {
        [domainOptions.gpconnect]: findAppointmentsGPConnect,
        [domainOptions.none]: findAppointmentsMigrator
    };
    return await map[api()]();
};

export const findAppointmentsGPConnect = async () => {
    const interactionId = 'urn:nhs:names:services:gpconnect:fhir:rest:search:patient_appointments-1';

    const headers = {
        ...commonHeaders,
        'Ssp-InteractionID': interactionId,
        'Authorization': `Bearer ${buildRequest()}`
    };

    const start = '2020-02-12';
    const end = '2020-02-22';

    const url = `${domain}/Patient/2/Appointment?start=ge${start}&start=le${end}`;

    const response = await superfetch({ url, headers, method: 'GET' });
    return gpconnectTransformer(response);
};

export const findAppointmentsMigrator = () => {
    return migratorTransformer(mockFindAppointments);
};