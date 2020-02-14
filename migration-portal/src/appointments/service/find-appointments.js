import { buildRequest } from '../../service/jwt';
import { commonHeaders, domain } from '../../service/common';
import { api, domainOptions } from '../../features';
import { gpconnectTransformer } from '../transformers/gpconnect';
import { migratorTransformer } from '../transformers/migrator';

export const getAppointment = async () => {
    const map = {
        [domainOptions.gpconnect]: getAppointmentGPConnect,
        [domainOptions.none]: getAppointmentMigrator
    };
    return await map[api()]();
};

export const getAppointmentGPConnect = async () => {
    const interactionId = 'urn:nhs:names:services:gpconnect:fhir:rest:search:patient_appointments-1';

    const headers = {
        ...commonHeaders,
        'Ssp-InteractionID': interactionId,
        'Authorization': `Bearer ${buildRequest()}`
    };

    const start = '2020-02-12';
    const end = '2020-02-22';

    const url = `${domain}/Patient/2/Appointment?start=ge${start}&start=le${end}`;

    const response = await fetch(url, {
        headers,
        method: 'GET'
    }).catch(error => {
        console.log('error', error);
    });

    return transformData(gpconnectTransformer, response);
};

export const getAppointmentMigrator = async () => {
    const url = `/mock/find-appointments.json`;
    
    const response = await fetch(url, {
        method: 'GET'
    }).catch(error => {
        console.log('error', error);
    });
    
    return transformData(migratorTransformer, response);
};

const transformData = async (transformer, raw) => {
    if (raw) {
        const data = await raw.json();
        return transformer(data);
    }
    return [];
};