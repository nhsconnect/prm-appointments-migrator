Appointment Migrator

This is the primary backend service for orchestrating transfer of appointments via GPConnect api of source and target systems.

To Hit Health endpoint

  GET http://localhost:5001/health

To initialise a dummy appointment in source system. (May need a few)

  POST http://localhost:5001/InitialiseDummyAppointments

    {
        "PatientId": "4",
        "Start": "2020-03-19T12:00:00.092Z",
        "End": "2020-03-19T12:10:00.092Z"
    }   

Find future appointments in source system

    GET https://localhost:5001/findappointments

    example response:

      [
        {
            "patient": "Mike MEAKIN",
            "patientId": 2,
            "slot": null,
            "start": "2020-03-19T09:00:00",
            "end": "2020-03-19T09:10:00",
            "location": null,
            "locationId": 17,
            "practitioner": "Kibo Slater",
            "practitionerId": 2,
            "description": "A appointment to discuss test data"
        },
        {
            "patient": "Mike MEAKIN",
            "patientId": 2,
            "slot": null,
            "start": "2020-03-19T09:10:00",
            "end": "2020-03-19T09:20:00",
            "location": null,
            "locationId": 17,
            "practitioner": "Kibo Slater",
            "practitionerId": 2,
            "description": "A follow-up appointment for tests."
        },
        {
            "patient": "Arnold OLLEY",
            "patientId": 4,
            "slot": null,
            "start": "2020-03-19T12:00:00",
            "end": "2020-03-19T12:10:00",
            "location": null,
            "locationId": 16,
            "practitioner": "Nichole Gilbert",
            "practitionerId": 1,
            "description": "A test appointment booked through Interactive Swagger API"
        }
  ]


