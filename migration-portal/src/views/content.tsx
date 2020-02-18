import React, { useState } from 'react';
import { Route, Switch } from 'react-router-dom';
import BookAppointments from './book-appointments';
import FindAppointments from './find-appointments';
import Finding from './finding';
import Transferring from './transferring';
import Upload from './upload';
import { publicPath } from '../config/env';

export const pendingStates = {
    finding: 'finding',
    transferring: 'transferring'
};

export default () => {
    const [numberAppts, setNumberAppts] = useState();
    const [interstitial, setInterstitial] = useState('');

    return <Switch>
        <Route exact path={`/${publicPath}`}>
            {interstitial === pendingStates.finding ? <Finding /> : <Upload startFinding={setInterstitial}/>}
        </Route>
        <Route path={`/${publicPath}/appointments`}>
            {interstitial === pendingStates.transferring
                ? <Transferring numberAppts={numberAppts} />
                : <FindAppointments startTransferring={setInterstitial} setNumberAppts={setNumberAppts} />
            }
        </Route>
        <Route path={`/${publicPath}/booked`}>
            <BookAppointments />
        </Route>
    </Switch>;
};