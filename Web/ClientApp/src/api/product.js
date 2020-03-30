/* Http */
import { get } from './http';

const application = 'http://localhost:49847';

/**
 * Gets product from api by code
 * @param {any} code
 */
export const getProductByID = async (code) => {
    return await get(application.concat(`/api/products/getProductByCode/${code}`));
};