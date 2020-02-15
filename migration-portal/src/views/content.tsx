import React from 'react';
import { Route, Switch } from 'react-router-dom';
import BookAppointments from './book-appointments';
import FindAppointments from './find-appointments';
import { publicPath } from '../config/env';

export default () => {
    return <Switch>
        <Route exact path={`/${publicPath}`}>
            <FindAppointments />
        </Route>
        <Route path={`/${publicPath}/booked`}>
            <BookAppointments />
        </Route>
    </Switch>;
};