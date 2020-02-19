import { commonHeaders, domain2 } from './common';
import { buildRequest } from './jwt';
import axios, { AxiosError } from 'axios';

export const bookAppointments = async ({
    start,
    end,
    slotId,
    patientId,
    locationId
} = {
        start: '2020-03-13T09:40:00+00:00',
        end: '2020-03-13T09:50:00+00:00',
        slotId: '115',
        patientId: '2',
        locationId: '17'
    }) => {

    const body = JSON.stringify({
        "resourceType": "Appointment",
        "meta": {
            "profile": [
                "https://fhir.nhs.uk/STU3/StructureDefinition/GPConnect-Appointment-1"
            ]
        },
        "contained": [
            {
                "resourceType": "Organization",
                "id": "1",
                "meta": {
                    "profile": [
                        "https://fhir.nhs.uk/STU3/StructureDefinition/CareConnect-GPC-Organization-1"
                    ]
                },
                "identifier": [
                    {
                        "system": "https://fhir.nhs.uk/Id/ods-organization-code",
                        "value": "GPC001"
                    }
                ],
                "name": "GP Connect Demonstrator",
                "telecom": [
                    {
                        "system": "phone",
                        "value": "0113 258 2569"
                    }
                ]
            }
        ],
        "status": "booked",
        "created": "2020-02-11T09:40:00+00:00",
        "description": "A test appointment booked from Appointments Portal 📓",
        "start": start,
        "end": end,
        "slot": [
            {
                "reference": `Slot/${slotId}`
            }
        ],
        "participant": [
            {
                "actor": {
                    "reference": `Patient/${patientId}`
                },
                "status": "accepted"
            },
            {
                "actor": {
                    "reference": `Location/${locationId}`
                },
                "status": "accepted"
            }
        ],
        "extension": [
            {
                "url": "https://fhir.nhs.uk/STU3/StructureDefinition/Extension-GPConnect-BookingOrganisation-1",
                "valueReference": {
                    "reference": "#1"
                }
            }
        ]
    });

    const interactionId = 'urn:nhs:names:services:gpconnect:fhir:rest:create:appointment-1';

    const headers = {
        ...commonHeaders,
        'Ssp-InteractionID': interactionId,
        'Authorization': `Bearer ${buildRequest()}`
    };

    const url = `${domain2}/Appointment`;

    // const response = await superfetch({ url, body, headers, method: 'POST' });
    // return gpconnectTransformer(response);

    const res = await axios.post(url, body, { headers }).catch((error: AxiosError) => {
        const { response } = error;
        return response || { data: error };
    });

    return res;
};


