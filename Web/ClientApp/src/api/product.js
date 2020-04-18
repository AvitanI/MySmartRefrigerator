/* Http */
import { get, API_RESOURCE } from './http';

/**
 * Gets product from api by code
 * @param {any} code
 */
export const getProductByID = async (code) => {
    return await get(API_RESOURCE(`/api/products/getProductByCode/${code}`));
};