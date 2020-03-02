import { getSlots } from '../services/slots';
import { domain1, port } from '../services/common';

export const findSlotsBookAppointments = async ({
    start,
    end,
    patientId
}) => {

        const slots = await getSlots({
            domain: domain1,
            port, start, end
        });

        return slots;

};


