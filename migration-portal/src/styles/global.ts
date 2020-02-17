import { css } from 'emotion';

export const marginBottom = {
    small: css({
        marginBottom: '1rem'
    }),
    regular: css({
        marginBottom: '2rem'
    }),
    large: css({
        marginBottom: '4rem'
    })
};

export const listReset = css({
    margin: '0',
    padding: '0',
    textIndent: '0',
    listStyleType: 'none',
});

export const float = {
    right: css({
        float: 'right'
    })
};

export const linkOverride = css({
    '&:hover': {
        boxShadow: '0 0 0 0px'
    },
    textDecoration: 'none'

});

export const hr = css({
    borderBottom: '4px solid #005eb8',
    marginBottom: '1rem'
});

export const boxShadow = '0px 0px 5px 0px rgba(0, 0, 0, 0.3)';