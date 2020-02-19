import axios, { AxiosRequestConfig, AxiosError } from 'axios';
import { buildRequest } from './jwt';

export const getSlots = async ({ domain, port }: { domain: string, port: string }) => {
    const payload: AxiosRequestConfig = {
        headers: {
            'accept': 'application/fhir+json',
            'Ssp-TraceID': '09a01679-2564-0fb4-5129-aecc81ea2706',
            'Ssp-From': 200000000359,
            'Ssp-To': 918999198993,
            'Ssp-InteractionID': 'urn:nhs:names:services:gpconnect:fhir:rest:search:slot-1',
            'Authorization': `Bearer ${buildRequest()}`
        },
        params: {
            start: 'ge2020-02-17',
            end: 'le2020-02-20',
            status: 'free',
            _include: 'Slot:schedule',
            searchFilter: 'https://fhir.nhs.uk/STU3/CodeSystem/GPConnect-OrganisationType-1|gp-practice'
        }
    };

    const url = `http://${domain}:${port}/gpconnect-demonstrator/v1/fhir/Slot`;
    const res = await axios.get(url, payload).catch((error: AxiosError) => {
        const { response } = error;
        return response || { data: error };
    });
    return res.data;
};
