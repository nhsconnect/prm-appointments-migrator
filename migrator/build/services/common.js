"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.commonHeaders = {
    'accept': 'application/fhir+json',
    'Ssp-TraceID': '09a01679-2564-0fb4-5129-aecc81ea2706',
    'Ssp-From': 200000000359,
    'Ssp-To': 918999198993
};
exports.path = '/gpconnect-demonstrator/v1/fhir';
exports.port = process.env.demonstratorport;
exports.domain2 = `http://${process.env.demonstrator2}:${exports.port}${exports.path}`;
exports.domain1 = `http://${process.env.demonstrator1}:${exports.port}${exports.path}`;
//# sourceMappingURL=common.js.map