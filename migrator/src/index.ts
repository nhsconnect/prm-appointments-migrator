import express from 'express';
import { buildRequest } from './jwt';
import { getSlots } from './services/slots';

export const app = express();
app.use((req, res, next) => {
  // console.log(req.path);
  next();
});
app.use(express.json());

app.get('/token', async (req, res) => {
  const response = buildRequest();
  // const response = await getSlots();
  res.send(response);
});

app.get('/token-write', async (req, res) => {
  const response = buildRequest(true);
  // const response = await getSlots();
  res.send(response);
});

app.get('/slots1', async (req, res) => {
  const domain = process.env.demonstrator1 as string;
  const port = process.env.demonstratorport as string;
  const response = await getSlots({ domain, port });
  res.send(response);
});

app.get('/slots2', async (req, res) => {
  const domain = process.env.demonstrator2 as string;
  const port = process.env.demonstratorport as string;
  const response = await getSlots({ domain, port});
  res.send(response);
});

app.get('/health', async (req, res, next) => {
  res.send({ version: process.env.VERSION || 'SNAPSHOT' });
});

app.listen(4010, () => {
  console.log(`🚀 Server ready at port http://localhost:4010`);
});
