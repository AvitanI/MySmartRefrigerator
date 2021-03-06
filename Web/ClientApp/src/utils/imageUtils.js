/**
 * Get icon url for map marker
 * @param {number} chainID
 */
export const getChainMapIconByChainID = (chainID) => {
    if(!chainID || chainID <= 0) {
        // TO-DO: return default icon
        return null;
    }

    switch(chainID) {
        case 1:
            return '/images/chainLogos/ShufersalLogo2.png';
        case 2:
            return '/images/chainLogos/RamiLeviLogo.png';
        default:
            return null;
    }
};