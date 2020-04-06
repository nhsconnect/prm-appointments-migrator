import React, { Dispatch, SetStateAction, useEffect, useState } from "react";
import Cards from "../components/cards";
import { findAppointments } from "../services/find-appointments";
import { marginBottom } from "../styles/global";
import { pendingStates } from "./content";

interface findAppointments {
  setNumberAppts: Dispatch<SetStateAction<string>>;
  startTransferring: Dispatch<SetStateAction<string>>;
  setAppointmentsHook: Dispatch<SetStateAction<never[]>>;
}

export default ({
  setNumberAppts,
  startTransferring,
  setAppointmentsHook
}: findAppointments) => {
  const [appointments, setAppointments] = useState([]);

  const getSlotService = async () => {
    const response = await findAppointments();
    setAppointments(response);
    setNumberAppts(response.length);
    setAppointmentsHook(response);
  };

  useEffect(() => {
    getSlotService();
  });

  return (
    <div className={marginBottom.large}>
      <p className={marginBottom.regular}>
        Found {appointments.length} in current solution between <b>today</b> and{" "}
        <b>13/04/2020</b>.
      </p>

        <button className="nhsuk-button" onClick={() => startTransferring(pendingStates.transferring)}>Transfer all appointments</button>
      <Cards items={appointments} />
    </div>
  );
};
