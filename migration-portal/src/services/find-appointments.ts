import { api, domainOptions } from '../config/features';
import { superfetch } from './superfetch';
import { migratorTransformer } from './transformers/migrator';
import { mockFindAppointments } from './mock/appointments';
import { domain } from './common';

export const findAppointments = async () => {
    const map = {
        [domainOptions.prod]: findAppointmentsParth,
        [domainOptions.none]: findAppointmentsMigrator
    };
    return await map[api()]();
};

export const findAppointmentsParth = async () => {
    const url = `${domain}/find-appointments`;

    const response = await superfetch({ url, method: 'GET' });
    return migratorTransformer(response);
};

export const findAppointmentsMigrator = async () => {
    const response = mockFindAppointments;
    return migratorTransformer(response);
};