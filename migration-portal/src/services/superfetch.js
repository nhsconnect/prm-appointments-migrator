export const superfetch = async ({ url, headers = {}, method = 'GET', body = {} }) => {
    const response = await fetch(url, {
        headers,
        method,
        body
    }).catch(error => {
        console.log('error', error);
    });

    if (response) {
        return await response.json();
    }
    return [];
};
