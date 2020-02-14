import React, { useEffect, useState } from 'react';
import Cards from '../components/cards';
import { bookAppointments } from '../services/book-appointments';
import { splitSuccessFail } from '../services/transformers/migrator';

export default () => {
    const [appointmentsSuccess, setAppointmentsSuccess] = useState([]);
    const [appointmentsFail, setAppointmentsFail] = useState([]);

    const getSlotService = async () => {
        const response = await bookAppointments();
        const success = response.filter(item => item.success);
        const fail = response.filter(item => !item.success);
        
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
