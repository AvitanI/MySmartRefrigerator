/* Http */
import { get, API_RESOURCE } from './http';

/**
 * Gets last updated product prices from api by code
 * @param {any} code
 */
export const getLastUpdatedProductPricesByCode = async (code) => {
    return await get(API_RESOURCE(`/api/productPrices/getLastUpdatedProductPricesByCode/${code}`));
};