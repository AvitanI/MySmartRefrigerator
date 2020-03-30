/* Http */
import { get } from './http';

const application = 'http://localhost:49847';

/**
 * Gets last updated product prices from api by code
 * @param {any} code
 */
export const getLastUpdatedProductPricesByCode = async (code) => {
    return await get(application.concat(`/api/productPrices/getLastUpdatedProductPricesByCode/${code}`));
};