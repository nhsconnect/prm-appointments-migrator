import React, { useState } from 'react';
import { Route, Switch } from 'react-router-dom';
import BookAppointments from './book-appointments';
import FindAppointments from './find-appointments';
import Finding from './finding';
import Transferring from './transferring';
import Upload from './upload';
import { publicPath } from '../config/env';

export default () => {
    const [numberAppts, setNumberAppts] = useState();

    return <Switch>
        <Route exact path={`/${publicPath}`}>
            <Upload />
        </Route>
        <Route path={`/${publicPath}/finding`}>
            <Finding />
        </Route>
        <Route path={`/${publicPath}/appointments`}>
            <FindAppointments setNumberAppts={setNumberAppts} />
        </Route>
        <Route path={`/${publicPath}/transferring`}>
            <Transferring numberAppts={numberAppts} />
        </Route>
        <Route path={`/${publicPath}/booked`}>
            <BookAppointments />
        </Route>
    </Switch>;
};