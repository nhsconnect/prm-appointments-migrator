import { migratorTransformer } from '../transformers/migrator';

export const bookAppointments = async () => {
    const url = `/mock/book-appointments.json`;

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