import axios, { AxiosRequestConfig, AxiosError } from 'axios';
import { buildRequest } from './jwt';
import { commonHeaders, domain1 } from './common';

export const getSlots = async ({ domain, port, start, end }: { domain: string, port: string, start: string, end: string }) => {
    const payload: AxiosRequestConfig = {
        headers: {
            ...commonHeaders,
            'Ssp-InteractionID': 'urn:nhs:names:services:gpconnect:fhir:rest:search:slot-1',
            'Authorization': `Bearer ${buildRequest()}`
        },
        params: {
            start,
            end,
            status: 'free',
            _include: 'Slot:schedule',
            searchFilter: 'https://fhir.nhs.uk/STU3/CodeSystem/GPConnect-OrganisationType-1|gp-practice'
        }
    };

    const url = `${domain1}/Slot`;
    const res = await axios.get(url, payload).catch((error: AxiosError) => {
        const { response } = error;
        return response || { data: error };
    });
    return res.data;
};
