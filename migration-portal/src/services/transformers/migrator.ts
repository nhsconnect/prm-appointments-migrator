export const migratorTransformer = (payload) => {
    return payload.map(({ start, end, ...rest }) => {
        return {
            start: new Date(start).toLocaleString(),
            end: new Date(end).toLocaleString().split(',')[1],
            ...rest
        };
    });
};

export const splitSuccessFail = (items) => {
    const success = items.filter(item => item.success);
    const fail = items.filter(item => !item.success);
    return { success, fail };
};