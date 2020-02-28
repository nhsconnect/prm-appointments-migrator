import { api, domainOptions } from '../config/features';
import { mockBookAppointments } from './mock/appointments';
import { superfetch } from './superfetch';
import { migratorTransformer } from './transformers/migrator';
import { domain } from '../config/env';

export const bookAppointments = async () => {
    const map = {
        [domainOptions.prod]: appMig,
        [domainOptions.none]: stub
    };
    return await map[api()]();
};

export const appMig = async () => {
    const url = `${domain}/book-appointments`;

    const response = await superfetch({ url, method: 'GET' });
    return migratorTransformer(response);
};

export const stub = () => {
    return migratorTransformer(mockBookAppointments);
};