const HTTP_GET = 'GET';
const HTTP_POST = 'POST';
// const HTTP_PUT = 'PUT';
// const HTTP_DELETE = 'DELETE';

/**
 * Get parsed fetch response by url and http options
 */
const parsedFetch = async (url, options) => {
    try {
        // Check for options
        if(!options || !Object.keys(options).length) {
            throw new Error('Options required');
        }

        const response = await fetch(url, options);
        const responseData = await response.json();
        
        return responseData;
      }
      catch(e) {
          console.log('e', e);

          return {
            Error: true,
            ErrorMessage: 'Error while trying process the request'
          };
      }
};

/**
 * Http Get
 */
export const get = async (url) => {
    return await parsedFetch(url, { method: HTTP_GET });
};

/**
 * Http Post
 */
export const post = async (url, data) => {
    return await parsedFetch(url, {
        method: HTTP_POST,
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });
};