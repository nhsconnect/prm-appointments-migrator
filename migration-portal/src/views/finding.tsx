import React from 'react';
import Progress from '../components/progressBar';
import { marginBottom } from '../styles/global';

export default () => {
    return (
        <div className={marginBottom.large}>
            <div className={marginBottom.regular}><b>Finding all appointments...</b></div>
            <Progress nextPage={'appointments'}/>
        </div>
    );
};
