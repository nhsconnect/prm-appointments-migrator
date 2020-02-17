import { css } from 'emotion';
import React, { useEffect, useReducer, useRef } from 'react';
import { useHistory } from 'react-router';
import { publicPath } from '../config/env';
import { boxShadow } from '../styles/global';

export default ({ nextPage }) => {

    const [percentage, setPercentage] = useReducer(acc => {
        if (acc < 100) {
            return acc + 10;
        }
        return acc < 100 ? acc + 10 : 100;
    }, 0);

    const perRef = useRef(0);

    const progressBg = css({
        width: '50%',
        backgroundColor: '#d8dde0',
        height: '1rem',
        borderRadius: '50px',
        boxShadow: boxShadow
    });
    
    const bar = css({
        width: `${percentage}%`,
        backgroundColor: '#005eb8',
        height: '100%',
        borderRadius: 'inherit',
        transition: 'width 0.5s ease-out'
    });

    const history = useHistory();

    useEffect(() => {
        const interval = setInterval(() => {
            if (perRef.current < 100) {
                perRef.current += 10;
                setPercentage();
            } else {
                clearInterval(interval);
                history.push(`/${publicPath}/${nextPage}`);
            }
        }, 500);
    }, [history]);

    return (
        <div className={progressBg}>
            <div className={bar}></div>
        </div>
    );
};
