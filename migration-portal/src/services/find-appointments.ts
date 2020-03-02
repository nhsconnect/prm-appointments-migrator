import { api, domainOptions, domain } from '../config/features';
import { superfetch } from './superfetch';
import { migratorTransformer } from './transformers/migrator';
import { mockFindAppointments } from './mock/appointments';

export const findAppointments = async () => {
    const map = {
        [domainOptions.prod]: findAppointmentsParth,
        [domainOptions.none]: findAppointmentsMigrator
    };
    return await map[api()]();
};

export const findAppointmentsParth = async () => {
    const url = `${domain()}/findappointments`;

    const response = await superfetch({ url, method: 'GET' });
    return migratorTransformer(response);
};

export const findAppointmentsMigrator = async () => {
    const response = mockFindAppointments;
    return migratorTransformer(response);
};