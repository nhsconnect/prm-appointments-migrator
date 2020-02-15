interface parameters {
    url: string;
    headers?: {};
    method: string;
    body?: string;
}

export const superfetch = async ({ url, headers = {}, method = 'GET', body = '' }: parameters) => {
    const response = await fetch(url, {
        headers,
        method,
        body
    }).catch((error) => {
        console.log('error', error);
    });

    if (response) {
        return await response.json();
    }
    return [];
};
