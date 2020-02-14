import React, { useEffect, useState } from 'react';
import Cards from '../cards';
import { bookAppointments } from './service/book-appointments';
import { splitSuccessFail } from './transformers/migrator';

export default () => {
    const [appointmentsSuccess, setAppointmentsSuccess] = useState([]);
    const [appointmentsFail, setAppointmentsFail] = useState([]);

    const getSlotService = async () => {
        const response = await bookAppointments();
        const { success, fail } = splitSuccessFail(response);
        
        setAppointmentsSuccess(success);
        setAppointmentsFail(fail);
    };

    useEffect(() => {
        getSlotService();
    }, []);

    return (
        <div class="mb4">
            <h2 class="mb2">Failed transfers</h2>
            <div class="mb2">{appointmentsFail.length} transfers failed.</div>
            <Cards items={ appointmentsFail } icon={ true } success={false} />
            <h2 class="mb2">Successful transfers</h2>
            <div class="mb2">{appointmentsSuccess.length} transfers successful.</div>
            <Cards items={ appointmentsSuccess } icon={ true } />
        </div>
    );
};
