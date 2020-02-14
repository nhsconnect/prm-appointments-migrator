import { buildRequest } from './jwt';
import { commonHeaders, domain } from './common';

export const getSlots = async () => {
    const slotInteractionId = 'urn:nhs:names:services:gpconnect:fhir:rest:search:slot-1';

    const headers = {
        ...commonHeaders,
        'Ssp-InteractionID': slotInteractionId,
        'Authorization': `Bearer ${buildRequest()}`
    };
    const params = {
        start: 'ge2020-02-13',
        end: 'le2020-02-27',
        status: 'free',
        _include: 'Slot:schedule',
        searchFilter: 'https://fhir.nhs.uk/STU3/CodeSystem/GPConnect-OrganisationType-1|gp-practice'
    };

    const url = new URL(`${domain}/Slot`);
    Object.keys(params).forEach(key => url.searchParams.append(key, params[key]))

    const res = await fetch(url, {
        headers,
        method: 'GET'
    }).catch(error => {
        const { response } = error;
        return response || { data: error };
    });

    const appts = await res.json();
    console.log(appts);
    return appts;
};