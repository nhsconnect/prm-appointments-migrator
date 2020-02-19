"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const axios_1 = __importDefault(require("axios"));
const jwt_1 = require("./jwt");
exports.getSlots = async ({ domain, port }) => {
    const payload = {
        headers: {
            'accept': 'application/fhir+json',
            'Ssp-TraceID': '09a01679-2564-0fb4-5129-aecc81ea2706',
            'Ssp-From': 200000000359,
            'Ssp-To': 918999198993,
            'Ssp-InteractionID': 'urn:nhs:names:services:gpconnect:fhir:rest:search:slot-1',
            'Authorization': `Bearer ${jwt_1.buildRequest()}`
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
    const res = await axios_1.default.get(url, payload).catch((error) => {
        const { response } = error;
        return response || { data: error };
    });
    return res.data;
};
//# sourceMappingURL=slots.js.map