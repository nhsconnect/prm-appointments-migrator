interface parameters {
    url: string;
    headers?: {};
    method: string;
    body?: string;
}

export const superfetch = async ({ url, headers = {}, method = 'GET', body}: parameters) => {
    const baseOptions: RequestInit = {
        headers,
        method
    };

    const options = body ? { ...baseOptions, body } : baseOptions;

    const response = await fetch(url, options).catch((error) => {
        console.log('error', error);
    });

    if (response) {
        return await response.json();
    }
    return [];
};
