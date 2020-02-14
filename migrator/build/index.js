"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const jwt_1 = require("./jwt");
exports.app = express_1.default();
exports.app.use((req, res, next) => {
    console.log(req.path);
    next();
});
exports.app.use(express_1.default.json());
exports.app.get('/token', async (req, res) => {
    const response = jwt_1.buildRequest();
    // const response = await getSlots();
    res.send(response);
});
exports.app.get('/token-write', async (req, res) => {
    const response = jwt_1.buildRequest(true);
    // const response = await getSlots();
    res.send(response);
});
exports.app.get('/health', async (req, res, next) => {
    res.send({ version: process.env.VERSION || 'SNAPSHOT' });
});
exports.app.listen(4010, () => {
    console.log(`🚀 Server ready at port http://localhost:4010`);
});
//# sourceMappingURL=index.js.map