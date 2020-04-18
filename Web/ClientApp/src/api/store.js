/* Http */
import { get, API_RESOURCE } from './http';

/**
 * Get all stores
 */
export const getStores = async () => {
    return await get(API_RESOURCE('/api/stores/getStores'));
};