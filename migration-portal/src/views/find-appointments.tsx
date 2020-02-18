import React, { Dispatch, SetStateAction, useEffect, useState } from 'react';
import Cards from '../components/cards';
import { findAppointments } from '../services/find-appointments';
import { linkOverride, marginBottom } from '../styles/global';
import { pendingStates } from './content';

interface findAppointments {
    setNumberAppts: Dispatch<SetStateAction<string>>,
    startTransferring: Dispatch<SetStateAction<string>>,
}

export default ({ setNumberAppts, startTransferring }: findAppointments) => {
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
            <a className={linkOverride} onClick={() => startTransferring(pendingStates.transferring)}>
                <button className="nhsuk-button">
                    Transfer all appointments
                </button>
            </a>
            <Cards items={appointments} />
        </div>
    );
};
