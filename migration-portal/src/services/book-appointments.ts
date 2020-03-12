import { api, domainOptions, domain } from '../config/features';
import { mockBookAppointments } from './mock/appointments';
import { superfetch } from './superfetch';
import { migratorTransformer } from './transformers/migrator';

export const bookAppointments = async (payload: {}[]) => {
    const map = {
        [domainOptions.prod]: appMig,
        [domainOptions.none]: stub
    };
    return await map[api()](payload);
};

export const appMig = async (payload: {}[]) => {
    const url = `${domain}/book-appointments`;


    const response = await superfetch({ url, body: JSON.stringify(payload), method: 'POST' });
    return migratorTransformer(response);
};

export const stub = () => {
    return migratorTransformer(mockBookAppointments);
};