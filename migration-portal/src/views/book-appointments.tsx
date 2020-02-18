import React, { useEffect, useState } from 'react';
import Cards from '../components/cards';
import { bookAppointments } from '../services/book-appointments';
import { marginBottom } from '../styles/global';

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
        <div className={marginBottom.large}>
            <h2 className={marginBottom.regular}>Failed transfers</h2>
            <div className={marginBottom.regular}>{appointmentsFail.length} transfers failed.</div>
            <Cards items={appointmentsFail} icon={true} success={false} />
            <h2 className={marginBottom.regular}>Successful transfers</h2>
            <div className={marginBottom.regular}>{appointmentsSuccess.length} transfers successful.</div>
            <Cards items={appointmentsSuccess} icon={true} />
        </div>
    );
};
