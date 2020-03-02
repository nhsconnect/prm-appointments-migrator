export const domainOptions = {
    prod: 'prod',
    none: 'none'
};

const features = {
    api: domainOptions.none,
    domain: ''
};

export const setFeatures = (featuresToSet: { [s: string]: unknown; } | ArrayLike<unknown>) => {
    Object.entries(featuresToSet).forEach(([key, value]) => {
        features[key] = value;
    });
};

export const api = () => {
    return domainOptions[features.api] || domainOptions.none
};

export const domain = () => {
    return `https://${features.domain}.ngrok.io`;
}