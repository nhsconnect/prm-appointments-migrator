"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const httpClient_1 = require("./httpClient");
const express_1 = __importDefault(require("express"));
function getAuth(header) {
    const token = header.split(/\s+/).pop();
    return token && Buffer.from(token, 'base64').toString().split(':');
}
describe('Http-client Integration', () => {
    let server;
    const port = 4005;
    const uri = '/test-url';
    beforeEach(() => {
        const app = express_1.default();
        app.use(express_1.default.json());
        app.post(uri, (request, response) => {
            response.send({ should: 'be received', serverReceived: request.body, headers: request.headers });
        });
        app.get(uri, (request, response) => {
            response.send({ cool: 'beans', serverReceived: request.body, headers: request.headers });
        });
        server = app.listen(port);
    });
    afterEach(() => server.close());
    describe('post', () => {
        console.log = jest.fn();
        it('can do a post to a valid url', async () => {
            const url = `http://127.0.0.1:${port}${uri}`;
            const payload = { a: 'b' };
            const response = await httpClient_1.post(url, payload);
            expect(response).toEqual({ should: 'be received', serverReceived: payload, headers: expect.anything() });
        });
        it('cannot do a post to an invalid url', async () => {
            const url = `http://127.0.0.1:${port}/test-url-not-exists`;
            const payload = { a: 'b' };
            await expect(httpClient_1.post(url, payload)).rejects.toThrowError('404');
        });
        it('Basic Auth only includes both username and password', async () => {
            const url = `http://127.0.0.1:${port}${uri}`;
            const payload = { a: 'b' };
            const response = await httpClient_1.post(url, payload, { auth: { username: 'user', password: 'pwd' } });
            const sentHeaders = response.headers;
            const auth = getAuth(sentHeaders.authorization);
            expect(auth[0]).toEqual('user');
            expect(auth[1]).toEqual('pwd');
        });
        it('is passed Headers', async () => {
            const url = `http://127.0.0.1:${port}${uri}`;
            const payload = { a: 'b' };
            const response = await httpClient_1.post(url, payload, { headers: { 'something': 'a', 'another-thing': 'b' } });
            const sentHeaders = response.headers;
            expect(sentHeaders.something).toEqual('a');
            expect(sentHeaders['another-thing']).toEqual('b');
        });
        it('call is logged', async () => {
            const url = `http://127.0.0.1:${port}${uri}`;
            const payload = { a: 'b' };
            const logOutput = `POST to ${url} with ${JSON.stringify(payload)}`;
            await httpClient_1.post(url, payload, { headers: { 'something': 'a', 'another-thing': 'b' } });
            expect(console.log).toHaveBeenCalledWith(expect.stringMatching(/POST.*/));
        });
        it('error is thrown', async () => {
            const url = `http://127.0.0.1:${port}/test-url-not-exists`;
            // tslint:disable-next-line:max-line-length
            await expect(httpClient_1.post(url, {}, {})).rejects.toThrow();
        });
    });
    describe('get', () => {
        console.log = jest.fn();
        it('should do a get to a uri', async () => {
            const url = `http://127.0.0.1:${port}${uri}`;
            const response = await httpClient_1.get({ url });
            expect(response).toEqual({ cool: 'beans', serverReceived: expect.anything(), headers: expect.anything() });
        });
        it('should pass cookies', async () => {
            const url = `http://127.0.0.1:${port}${uri}`;
            const response = await httpClient_1.get({ url, options: { cookies: { foo: 'bar' } } });
            expect(response).toEqual({
                cool: 'beans',
                serverReceived: expect.anything(),
                headers: expect.objectContaining({
                    cookie: 'foo=bar'
                })
            });
        });
        it('should pass headers to needle', async () => {
            const url = `http://127.0.0.1:${port}${uri}`;
            const response = await httpClient_1.get({ url, options: { headers: { schwemberhead: 'test' } }
            });
            expect(response).toEqual({
                cool: 'beans',
                serverReceived: expect.anything(),
                headers: expect.objectContaining({
                    schwemberhead: 'test'
                })
            });
        });
        it('should throw if the request fails', async () => {
            await expect(httpClient_1.get({ url: `http://127.0.0.1:${port}/test-url-not-exists` }))
                .rejects.toThrowError('404');
        });
        it('call is logged', async () => {
            const url = `http://127.0.0.1:${port}${uri}`;
            const logOutput = `GET ${url}`;
            await httpClient_1.get({ url, options: { headers: { 'something': 'a', 'another-thing': 'b' } } });
            expect(console.log).toHaveBeenCalledWith(expect.stringMatching(/GET.*/));
        });
        it('error is thrown', async () => {
            const url = `http://127.0.0.1:${port}/test-url-not-exists`;
            // tslint:disable-next-line:max-line-length
            await expect(httpClient_1.get({ url, options: { headers: { 'something': 'a', 'another-thing': 'b' } } }))
                .rejects.toThrow();
        });
    });
});
//# sourceMappingURL=httpClient.integration.test.js.map