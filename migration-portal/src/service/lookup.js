import { host, local } from '../features';
import { lookupData } from './data';
import { endpoints } from '../env';

const lookupService = async (nhsNumber) => {
  const response = await fetch(`${host()}/${endpoints.patient}/${nhsNumber}`);
  return await response.json();
};

export const lookup = async (nhsNumber) => {
  return host() === ''
    ? lookupData.find(item => item.nhsNumber === nhsNumber)
    : lookupService(nhsNumber);
};