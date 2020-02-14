import React from 'react';
import { Route, Switch, useHistory } from 'react-router-dom';
import Appointments from '../appointments/appointments';
import AppointmentsNew from '../appointments/book-appointments';
import { publicPath } from '../env';

export default () => {
    const history = useHistory();

    return <Switch>
        <Route exact path={`/${publicPath}`}>
            <Appointments />
        </Route>
        <Route path={`/${publicPath}/new`}>
            <AppointmentsNew />
        </Route>
        {/* <Route path="/success">
            <Success />
        </Route> */}
    </Switch>;
};


const validateNhsNumber = (nhsNumber) => {
    const nhsNumRegex = /^\d{10}$/;
    if (!nhsNumRegex.test(nhsNumber)) {
        return "No Patient found with that NHS Number";
    } else if (nhsNumber.charAt(0) === "9") {
        return "Patient is not in your practice";
    } else {
        return "";
    }
};