import { css } from 'emotion';
import React, { Fragment } from 'react';
import { boxShadow, marginBottom } from '../styles/global';
import { Link } from 'react-router-dom/cjs/react-router-dom.min';
import { publicPath } from '../config/env';


export default () => {
    const border = css({
        padding: '1rem',
        borderRadius: '1rem',
        fontSize: '1rem',
        fontWeight: 'bold',
        boxShadow,
        width: '50%'
    });

    return (
        <Fragment>
            <div className={marginBottom.small}>Using patient list from:</div>
            <p className={border}>
                patient.list.csv
            </p>
            <div className="nhsuk-action-link">
                <Link className="nhsuk-action-link__link" to={`/${publicPath}/finding`}>
                    <svg className="nhsuk-icon nhsuk-icon__arrow-right-circle" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" aria-hidden="true">
                        <path d="M0 0h24v24H0z" fill="none"></path>
                        <path d="M12 2a10 10 0 0 0-9.95 9h11.64L9.74 7.05a1 1 0 0 1 1.41-1.41l5.66 5.65a1 1 0 0 1 0 1.42l-5.66 5.65a1 1 0 0 1-1.41 0 1 1 0 0 1 0-1.41L13.69 13H2.05A10 10 0 1 0 12 2z"></path>
                    </svg>
                    <span className="nhsuk-action-link__text">Find all appointments</span>
                </Link>
            </div>
        </Fragment>
    );
};
