export const parthAppointments = [
    {
        "patient": null,
        "patientId": 1,
        "slot": null,
        "start": "2020-03-03T14:20:00",
        "end": "2020-03-03T14:30:00",
        "location": null,
        "locationId": 16,
        "practitioner": null,
        "practitionerId": 1,
        "description": "A test appointment booked through Interactive Swagger API"
    },
    {
        "patient": null,
        "patientId": 2,
        "slot": null,
        "start": "2020-03-03T09:00:00",
        "end": "2020-03-03T09:10:00",
        "location": null,
        "locationId": 17,
        "practitioner": null,
        "practitionerId": 2,
        "description": "A appointment to discuss test data"
    },
    {
        "patient": null,
        "patientId": 2,
        "slot": null,
        "start": "2020-03-03T09:10:00",
        "end": "2020-03-03T09:20:00",
        "location": null,
        "locationId": 17,
        "practitioner": null,
        "practitionerId": 2,
        "description": "A follow-up appointment for tests."
    },
    {
        "patient": null,
        "patientId": 2,
        "slot": null,
        "start": "2020-03-03T11:20:00",
        "end": "2020-03-03T11:30:00",
        "location": null,
        "locationId": 16,
        "practitioner": null,
        "practitionerId": 1,
        "description": "A test appointment booked through Interactive Swagger API"
    },
    {
        "patient": null,
        "patientId": 2,
        "slot": null,
        "start": "2020-03-03T11:20:00",
        "end": "2020-03-03T11:30:00",
        "location": null,
        "locationId": 16,
        "practitioner": null,
        "practitionerId": 1,
        "description": "A test appointment booked through Interactive Swagger API"
    },
    {
        "patient": null,
        "patientId": 3,
        "slot": null,
        "start": "2020-03-03T14:20:00",
        "end": "2020-03-03T14:30:00",
        "location": null,
        "locationId": 16,
        "practitioner": null,
        "practitionerId": 1,
        "description": "A test appointment booked through Interactive Swagger API"
    }
];

export const mockFindAppointments = [
    {
        description: 'A appointment to discuss test data.',
        start: '2020-02-13T09:00:00+00:00',
        end: '2020-02-13T09:10:00+00:00',
        minutesDuration: 10,
        schedule: 'Schedule 12 for phone appointments with staff nurse',
        patientId: 'Laura Barry',
        location: 'The Hepworth Surgery',
        practitioner: 'Nichole Gilbert (G13579135)'
    },
    {
        description: 'A follow-up appointment for tests.',
        start: '2020-02-13T09:00:00+00:00',
        end: '2020-02-13T09:10:00+00:00',
        minutesDuration: 10,
        schedule: 'Schedule 9 for general appointments',
        patientId: 'Les Fawcett',
        location: 'The Hockey Surgery Annex',
        practitioner: 'Dr Melissa Parsons (G11111116)'
    },
    {
        description: 'A appointment to discuss test data.',
        start: '2020-02-13T09:00:00+00:00',
        end: '2020-02-13T09:10:00+00:00',
        minutesDuration: 10,
        schedule: 'Schedule 12 for phone appointments with staff nurse',
        patientId: 'Emily Margo',
        location: 'The Hepworth Surgery',
        practitioner: 'Nichole Gilbert (G13579135)'
    },
    {
        description: 'A follow-up appointment for tests.',
        start: '2020-02-13T09:00:00+00:00',
        end: '2020-02-13T09:10:00+00:00',
        minutesDuration: 10,
        schedule: 'Schedule 9 for general appointments',
        patientId: 'Parth Aggarwal',
        location: 'The Hockey Surgery Annex',
        practitioner: 'Dr Melissa Parsons (G11111116)'
    },
    {
        description: 'A appointment to discuss test data.',
        start: '2020-02-13T09:00:00+00:00',
        end: '2020-02-13T09:10:00+00:00',
        minutesDuration: 10,
        schedule: 'Schedule 12 for phone appointments with staff nurse',
        patientId: 'Natasha Desai',
        location: 'The Hepworth Surgery',
        practitioner: 'Nichole Gilbert (G13579135)'
    },
    {
        description: 'A follow-up appointment for tests.',
        start: '2020-02-13T09:00:00+00:00',
        end: '2020-02-13T09:10:00+00:00',
        minutesDuration: 10,
        schedule: 'Schedule 9 for general appointments',
        patientId: 'Tina Coxhead',
        location: 'The Hockey Surgery Annex',
        practitioner: 'Dr Melissa Parsons (G11111116)'
    }
];

export const mockBookAppointments = [
    {
        "success": false,
        "errorMessage": "Could not find corresponding slot",
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
        "success": false,
        "errorMessage": "Could not find corresponding slot",
        "description": "A follow-up appointment for tests.",
        "start": "2020-02-13T09:00:00+00:00",
        "end": "2020-02-13T09:10:00+00:00",
        "minutesDuration": 10,
        "schedule": "Schedule 9 for general appointments",
        "patientId": "Les Fawcett",
        "location": "The Hockey Surgery Annex",
        "practitioner": "Dr Melissa Parsons (G11111116)"
    },
    {
        "success": true,
        "errorMessage": "",
        "description": "A appointment to discuss test data.",
        "start": "2020-02-13T09:00:00+00:00",
        "end": "2020-02-13T09:10:00+00:00",
        "minutesDuration": 10,
        "schedule": "Schedule 12 for phone appointments with staff nurse",
        "patientId": "Emily Margo",
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
        "patientId": "Parth Aggarwal",
        "location": "The Hockey Surgery Annex",
        "practitioner": "Dr Melissa Parsons (G11111116)"
    },
    {
        "success": true,
        "errorMessage": "",
        "description": "A appointment to discuss test data.",
        "start": "2020-02-13T09:00:00+00:00",
        "end": "2020-02-13T09:10:00+00:00",
        "minutesDuration": 10,
        "schedule": "Schedule 12 for phone appointments with staff nurse",
        "patientId": "Natasha Desai",
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
        "patientId": "Tina Coxhead",
        "location": "The Hockey Surgery Annex",
        "practitioner": "Dr Melissa Parsons (G11111116)"
    }
];