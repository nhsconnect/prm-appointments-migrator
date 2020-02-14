"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
jest.mock('needle');
const apollo_server_1 = require("apollo-server");
const httpClient_1 = require("./httpClient");
const needle_1 = __importDefault(require("needle"));
function getAuth(header) {
    const token = header.split(/\s+/).pop();
    return token && Buffer.from(token, 'base64').toString().split(':');
}
describe('HttpClient Unit test', () => {
    it('post() throws ApolloError when needle throws an Error', async () => {
        needle_1.default
            .mockRejectedValueOnce(new Error('A disaster happened'));
        try {
            await httpClient_1.post('http://some.url', '💣');
            fail('needle is expected to throw an exception, but the exception is never thrown ');
        }
        catch (expectedException) {
            expect(expectedException).toBeInstanceOf(apollo_server_1.ApolloError);
        }
    });
    it('get() throws ApolloError when needle throws an Error', async () => {
        needle_1.default.get.mockImplementationOnce((url, options, cb) => {
            throw new Error('A disaster happened');
        });
        try {
            await httpClient_1.get({ url: 'http://some.url' });
            fail('needle is expected to throw an exception, but the exception is never thrown ');
        }
        catch (expectedException) {
            expect(expectedException).toBeInstanceOf(apollo_server_1.ApolloError);
        }
    });
});
//# sourceMappingURL=httpClient.test.js.map