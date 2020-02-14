import React, { useEffect, useState } from 'react';
import Cards from '../cards';
import { getAppointment } from './service/find-appointments';
import { Link } from 'react-router-dom';
import { publicPath } from '../env';

export default () => {
    const [appointments, setAppointments] = useState([]);

    const getSlotService = async () => {
        const response = await getAppointment();
        setAppointments(response);
    };

    useEffect(() => {
        getSlotService();
    }, []);

    return (
        <div className="mb4">
            <p class="mb2">Found {appointments.length} appointments in current solution between <b>today</b> and <b>13/04/2020</b>.
            </p>
            <Link className="nhsuk-link-override" to={`/${publicPath}/new`}>
                <button className="nhsuk-button">
                    Transfer all appointments
            </button>
            </Link>
            <Cards items={appointments} />
        </div>
    );
};
