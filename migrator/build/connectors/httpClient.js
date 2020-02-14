"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const needle_1 = __importDefault(require("needle"));
const apollo_server_1 = require("apollo-server");
async function post(url, payload, { headers, auth } = {
    headers: {},
    auth: {}
}) {
    const httpVerb = 'POST';
    let response;
    try {
        response = await needle_1.default('post', url, payload, { ...auth, headers, json: true });
        const log = {
            method: httpVerb,
            url,
            payload,
            response: response.body
        };
        console.log(JSON.stringify(log));
    }
    catch (e) {
        const errorLog = {
            level: 'ERROR',
            method: httpVerb,
            url,
            stack: e.stack
        };
        throw new apollo_server_1.ApolloError(JSON.stringify(errorLog));
    }
    const { statusCode, body } = response;
    if (statusCode && statusCode >= 400) {
        const statusErrorLog = {
            level: 'DOWNSTREAM_ERROR',
            statusCode,
            method: httpVerb,
            url,
            body
        };
        throw new apollo_server_1.ApolloError(JSON.stringify(statusErrorLog));
    }
    return body;
}
exports.post = post;
async function get({ url, data = {}, options = {} }) {
    const httpVerb = 'GET';
    let response;
    try {
        response = await new Promise((resolve, reject) => {
            needle_1.default.get(url, options, (err, resp) => {
                if (err) {
                    reject(err);
                }
                else {
                    resolve(resp);
                }
            });
        });
        const log = {
            method: httpVerb,
            url,
            response: response.body
        };
        console.log(JSON.stringify(log));
    }
    catch (e) {
        const errorLog = {
            level: 'ERROR',
            method: httpVerb,
            url,
            stack: e.stack
        };
        throw new apollo_server_1.ApolloError(JSON.stringify(errorLog));
    }
    const { statusCode, body } = response;
    if (statusCode && statusCode >= 400) {
        const statusErrorLog = {
            level: 'DOWNSTREAM_ERROR',
            statusCode,
            method: httpVerb,
            url,
            body
        };
        // tslint:disable-next-line: max-line-length
        throw new apollo_server_1.ApolloError(JSON.stringify(statusErrorLog));
    }
    return body;
}
exports.get = get;
//# sourceMappingURL=httpClient.js.map