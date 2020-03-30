/* Http */
import { get } from './http';

const application = 'http://localhost:49847';

/**
 * Get all stores
 */
export const getStores = async () => {
    return await get(application.concat('/api/stores/getStores'));
};