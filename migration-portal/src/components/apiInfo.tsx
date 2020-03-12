import { css } from 'emotion';
import React from 'react';
import { domain } from '../config/features';

const apiInfoStyle = {
    marginBottom: '0px!important',
    color: 'lightgray'
};

export default () => {
    return <div className={`${css(apiInfoStyle)} nhsuk-body-s`}>
        API: {domain()}
    </div>
};
