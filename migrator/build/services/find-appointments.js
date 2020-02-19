"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const axios_1 = __importDefault(require("axios"));
const common_1 = require("./common");
const jwt_1 = require("./jwt");
exports.findAppointments = async () => {
    const interactionId = 'urn:nhs:names:services:gpconnect:fhir:rest:search:patient_appointments-1';
    const headers = {
        ...common_1.commonHeaders,
        'Ssp-InteractionID': interactionId,
        'Authorization': `Bearer ${jwt_1.buildRequest()}`
    };
    const start = '2020-02-18';
    const end = '2020-02-22';
    const url = `${common_1.domain1}/Patient/2/Appointment?start=ge${start}&start=le${end}`;
    const res = await axios_1.default.get(url, { headers }).catch((error) => {
        const { response } = error;
        return response || { data: error };
    });
    return transform(res.data);
};
const transform = (payload) => {
    const { entry: appointments } = payload;
    const lift = appointments
        .map(appointment => appointment.resource)
        .sort((a, b) => Number(a.id) - Number(b.id));
    const flatten = lift.map(({ id, description, start, end, minutesDuration, slot, participant }) => {
        return {
            patientId: participant[0].actor.reference,
            slot: slot[0].reference,
            location: participant[1].actor.reference,
            practitioner: participant[2].actor.reference,
            start,
            end,
            minutesDuration,
            description
        };
    });
    return flatten;
};
//# sourceMappingURL=find-appointments.js.map