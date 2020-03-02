import React, { useState, Fragment } from 'react';
import { Route, Switch } from 'react-router-dom';
import BookAppointments from './book-appointments';
import FindAppointments from './find-appointments';
import Finding from './finding';
import Transferring from './transferring';
import Upload from './upload';
import { publicPath } from '../config/env';
import ApiInfo from '../components/apiInfo';
import { api } from '../config/features';

export const pendingStates = {
    finding: 'finding',
    transferring: 'transferring'
};

export default () => {
    const [numberAppts, setNumberAppts] = useState();
    const [interstitial, setInterstitial] = useState('');
    const [appointments, setAppointments] = useState([]);

    return <Fragment>
        <Switch>
            <Route exact path={`/${publicPath}`}>
                {interstitial === pendingStates.finding ? <Finding /> : <Upload startFinding={setInterstitial} />}
            </Route>
            <Route path={`/${publicPath}/appointments`}>
                {interstitial === pendingStates.transferring
                    ? <Transferring numberAppts={numberAppts} />
                    : <FindAppointments startTransferring={setInterstitial} setNumberAppts={setNumberAppts} setAppointmentsHook={setAppointments}/>
                }
            </Route>
            <Route path={`/${publicPath}/booked`}>
                <BookAppointments appointments={appointments}/>
            </Route>
        </Switch>
        {api() !== 'none' && <ApiInfo />}
    </Fragment>;
};