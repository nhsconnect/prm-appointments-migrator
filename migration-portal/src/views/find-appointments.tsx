import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { publicPath } from '../config/env';
import { marginBottom, linkOverride } from '../styles/global';
import { findAppointments } from '../services/find-appointments';
import Cards from '../components/cards';

export default ({ setNumberAppts }) => {
    const [appointments, setAppointments] = useState([]);

    const getSlotService = async () => {
        const response = await findAppointments();
        setAppointments(response);
        setNumberAppts(response.length);
    };

    useEffect(() => {
        getSlotService();
    }, []);

    return (
        <div className={marginBottom.large}>
            <p className={marginBottom.regular}>Found {appointments.length} in current solution between <b>today</b> and <b>13/04/2020</b>.
            </p>
            <Link className={linkOverride} to={`/${publicPath}/transferring`}>
                <button className="nhsuk-button">
                    Transfer all appointments
            </button>
            </Link>
            <Cards items={appointments} />
        </div>
    );
};
