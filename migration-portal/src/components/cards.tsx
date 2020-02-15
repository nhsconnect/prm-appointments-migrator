import React from 'react';

export default ({ items, icon = false, success = true }) => {

    const renderTickOrCross = (success) => {
        const tick = <svg className="right nhsuk-icon nhsuk-icon__tick" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" aria-hidden="true">
            <path stroke-width="4" stroke-linecap="round" d="M18.4 7.8l-8.5 8.4L5.6 12"></path>
        </svg>;

        const cross = <svg className="right nhsuk-icon nhsuk-icon__cross" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" aria-hidden="true">
            <path d="M17 18.5c-.4 0-.8-.1-1.1-.4l-10-10c-.6-.6-.6-1.6 0-2.1.6-.6 1.5-.6 2.1 0l10 10c.6.6.6 1.5 0 2.1-.3.3-.6.4-1 .4z"></path>
            <path d="M7 18.5c-.4 0-.8-.1-1.1-.4-.6-.6-.6-1.5 0-2.1l10-10c.6-.6 1.5-.6 2.1 0 .6.6.6 1.5 0 2.1l-10 10c-.3.3-.6.4-1 .4z"></path>
        </svg>;

        const icon = {
            'true': tick,
            'false': cross,
        }[success.toString()];

        return icon;

    }

    const buildCards = (items) => {
        return items.map(item => {
            const { practitioner, location, start, end, description, patientId, schedule } = item;
            return (
                <li className="mb4">
                    <div>
                        <hr></hr>
                        {icon && renderTickOrCross(success)}
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
        <ul className="list-reset">
            <div className="mb4">
                {buildCards(items)}
            </div>
        </ul>
    );
};
