import { splitSuccessFail } from './migrator';

const fixture = [
    {
        "success": false,
        "errorMessage": "",
        "description": "A appointment to discuss test data.",
        "start": "2020-02-13T09:00:00+00:00",
        "end": "2020-02-13T09:10:00+00:00",
        "minutesDuration": 10,
        "schedule": "Schedule 12 for phone appointments with staff nurse",
        "patientId": "Laura Barry",
        "location": "The Hepworth Surgery",
        "practitioner": "Nichole Gilbert (G13579135)"
    },
    {
        "success": true,
        "errorMessage": "",
        "description": "A follow-up appointment for tests.",
        "start": "2020-02-13T09:00:00+00:00",
        "end": "2020-02-13T09:10:00+00:00",
        "minutesDuration": 10,
        "schedule": "Schedule 9 for general appointments",
        "patientId": "Les Fawcett",
        "location": "The Hockey Surgery Annex",
        "practitioner": "Dr Melissa Parsons (G11111116)"
    }
];

describe('transform data from service', () => {
    it('split into two arrays of success and fail', async () => {
        const {success, fail} = splitSuccessFail(fixture);
        expect(success.length).toEqual(1);
        expect(success[0].patientId).toEqual("Les Fawcett");
        expect(fail.length).toEqual(1);
        expect(fail[0].patientId).toEqual("Laura Barry");
    });
});



