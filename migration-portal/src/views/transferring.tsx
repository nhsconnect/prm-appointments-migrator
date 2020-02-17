import React from 'react';
import Progress from '../components/progressBar';
import { marginBottom } from '../styles/global';

export default ({ numberAppts }) => {
    return (
        <div className={marginBottom.large}>
            <div className={marginBottom.regular}><b>Transferring {numberAppts} appointments...</b></div>
            <Progress nextPage={'booked'}/>
        </div>
    );
};
