"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const crypto_js_1 = __importDefault(require("crypto-js"));
exports.buildRequest = (write = false) => {
    // Construct the JWT token for the request
    const currentTime = new Date();
    const expiryTime = new Date(currentTime.getTime() + 300000); // 5 mins after current time
    const jwtCreationTime = Math.round(currentTime.getTime() / 1000);
    const jwtExpiryTime = Math.round(expiryTime.getTime() / 1000);
    const targetURI = 'providerURL_1_x_x';
    const requesting_organization_ODS_Code = 'requesting_organization_ODS_Code';
    const payload = {
        'iss': 'http://gpconnect-postman-url',
        'sub': '1',
        'aud': targetURI,
        'exp': jwtExpiryTime,
        'iat': jwtCreationTime,
        'reason_for_request': 'directcare',
        'requested_scope': write ? 'patient/*.write' : 'patient/*.read',
        'requesting_device': {
            'resourceType': 'Device',
            'id': '1',
            'identifier': [
                {
                    'system': 'Web Interface',
                    'value': 'Postman example consumer'
                }
            ],
            'model': 'Postman',
            'version': '1.0'
        },
        'requesting_organization': {
            'resourceType': 'Organization',
            'identifier': [
                {
                    'system': 'https://fhir.nhs.uk/Id/ods-organization-code',
                    'value': requesting_organization_ODS_Code
                }
            ],
            'name': 'Postman Organization'
        },
        'requesting_practitioner': {
            'resourceType': 'Practitioner',
            'id': '1',
            'identifier': [
                {
                    'system': 'https://fhir.nhs.uk/Id/sds-user-id',
                    'value': 'G13579135'
                },
                {
                    'system': 'https://fhir.nhs.uk/Id/sds-role-profile-id',
                    'value': '111111111'
                },
            ],
            'name': [{
                    'family': 'Demonstrator',
                    'given': [
                        'GPConnect'
                    ],
                    'prefix': [
                        'Mr'
                    ]
                }]
        }
    };
    // Encode the JWT data into the base64url encoded string
    const stringifiedPayload = crypto_js_1.default.enc.Utf8.parse(JSON.stringify(payload));
    const encodedPayload = base64url(stringifiedPayload);
    return 'eyJhbGciOiJub25lIiwidHlwIjoiSldUIn0.' + encodedPayload + '.';
};
const base64url = source => {
    // Encode in classical base64
    return crypto_js_1.default.enc.Base64.stringify(source)
        .replace(/=+$/, '')
        // Replace characters according to base64url specifications
        .replace(/\\+/g, '-')
        .replace(/\//g, '_');
};
//# sourceMappingURL=jwt.js.map