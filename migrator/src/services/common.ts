export const commonHeaders = {
    'accept': 'application/fhir+json',
    'Ssp-TraceID': '09a01679-2564-0fb4-5129-aecc81ea2706',
    'Ssp-From': 200000000359,
    'Ssp-To': 918999198993
};

export const path = '/gpconnect-demonstrator/v1/fhir';
export const port = process.env.demonstratorport || '9000';

export const domain2 = `http://${process.env.demonstrator2}:${port}${path}`;
export const domain1 = `http://${process.env.demonstrator1}:${port}${path}`;