"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const base64url_1 = __importDefault(require("base64url"));
exports.buildRequest = (write = false) => {
    // Construct the JWT token for the request
    const currentTime = new Date();
    const expiryTime = new Date(currentTime.getTime() + 300000); // 5 mins after current time
    const jwtCreationTime = Math.round(currentTime.getTime() / 1000);
    const jwtExpiryTime = Math.round(expiryTime.getTime() / 1000);
    const targetURI = "providerURL_1_x_x";
    const requesting_organization_ODS_Code = "requesting_organization_ODS_Code";
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
                    "system": "Web Interface",
                    "value": "Postman example consumer"
                }
            ],
            "model": "Postman",
            "version": "1.0"
        },
        "requesting_organization": {
            "resourceType": "Organization",
            "identifier": [
                {
                    "system": "https://fhir.nhs.uk/Id/ods-organization-code",
                    "value": requesting_organization_ODS_Code
                }
            ],
            "name": "Postman Organization"
        },
        "requesting_practitioner": {
            "resourceType": "Practitioner",
            "id": "1",
            "identifier": [
                {
                    "system": "https://fhir.nhs.uk/Id/sds-user-id",
                    "value": "G13579135"
                },
                {
                    "system": "https://fhir.nhs.uk/Id/sds-role-profile-id",
                    "value": "111111111"
                },
            ],
            "name": [{
                    "family": "Demonstrator",
                    "given": [
                        "GPConnect"
                    ],
                    "prefix": [
                        "Mr"
                    ]
                }]
        }
    };
    // Encode the JWT data into the base64url encoded string
    const encodedPayload = base64url_1.default(JSON.stringify(payload));
    return `eyJhbGciOiJub25lIiwidHlwIjoiSldUIn0.${encodedPayload}.`;
};
//# sourceMappingURL=jwt.js.map