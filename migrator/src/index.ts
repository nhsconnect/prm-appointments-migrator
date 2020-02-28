import express from 'express';
import { buildRequest } from './services/jwt';
import { getSlots } from './services/slots';
import { mockFindAppointments, mockBookAppointments } from './mock/appointments';
import cors from 'cors';

export const app = express();
app.use((req, res, next) => {
  next();
});
app.use(express.json());
app.use(cors())

app.get('/token', async (req, res) => {
  const response = buildRequest();
  res.send(response);
});

app.get('/token-write', async (req, res) => {
  const response = buildRequest(true);
  res.send(response);
});

app.get('/find-appointments', async (req, res) => {
  const response = mockFindAppointments;
  res.send(response);
});


app.get('/book-appointments', async (req, res) => {
  const response = mockBookAppointments;
  res.send(response);
});

app.get('/health', async (req, res, next) => {
  res.send({ version: process.env.VERSION || 'SNAPSHOT' });
});

app.listen(4010, () => {
  console.log(`🚀 Server ready at port http://localhost:4010`);
});
