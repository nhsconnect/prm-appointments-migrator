import { css, Interpolation } from 'emotion';
import React from 'react';
import { hr, listReset, marginBottom } from '../styles/global';

export default ({ items, icon = false, success = true }) => {

    const renderTickOrCross = success => {
        const tick = <svg className={`${verticalAlign} nhsuk-icon nhsuk-icon__tick`} xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" aria-hidden="true">
            <path strokeWidth="4" strokeLinecap="round" d="M18.4 7.8l-8.5 8.4L5.6 12"></path>
        </svg>;

        const cross = <svg className={`${verticalAlign} nhsuk-icon nhsuk-icon__cross`} xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" aria-hidden="true">
            <path d="M17 18.5c-.4 0-.8-.1-1.1-.4l-10-10c-.6-.6-.6-1.6 0-2.1.6-.6 1.5-.6 2.1 0l10 10c.6.6.6 1.5 0 2.1-.3.3-.6.4-1 .4z"></path>
            <path d="M7 18.5c-.4 0-.8-.1-1.1-.4-.6-.6-.6-1.5 0-2.1l10-10c.6-.6 1.5-.6 2.1 0 .6.6.6 1.5 0 2.1l-10 10c-.3.3-.6.4-1 .4z"></path>
        </svg>;

        const icon = {
            'true': tick,
            'false': cross,
        }[success.toString()];

        return icon;
    };

    const notificationColour = () => {
        return success ? 'transparent' : '#ffb5b5';
    }

    const notification: Interpolation = css({
        // height: '3rem',
        width: '100%',
        backgroundColor: notificationColour(),
        marginTop: '-1rem',
        marginBottom: '1rem',
        padding: '0.5rem'
    });

    const verticalAlign: Interpolation = css({
        verticalAlign: 'middle'
    });

    const buildCards = (items) => {
        return items.map(item => {
            const { practitioner, location, start, end, description, patientId, schedule, errorMessage } = item;
            return (
                <li className={marginBottom.large} key={patientId}>
                    <div>
                        <hr className={hr}></hr>
                        <div className={notification}>
                            {icon && renderTickOrCross(success)}
                            <span className={verticalAlign}><em>{errorMessage}</em></span>
                        </div>
                        <h4>
                            {patientId}
                        </h4>
                        <ul>
                            <li>{start} ➡ {end}</li>
                            <li>{practitioner}</li>
                            <li>{location}</li>
                            <li>{schedule}</li>
                        </ul>
                        <div>{description}</div>
                    </div>
                </li>
            )
        });
    };

    return (
        <ul className={listReset}>
            <div className={marginBottom.large}>
                {buildCards(items)}
            </div>
        </ul>
    );
};
