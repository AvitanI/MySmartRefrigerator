/* React */
import React, { useState, useContext } from 'react';

/* Material UI */
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import CircularProgress from '@material-ui/core/CircularProgress';
import { useSnackbar } from 'notistack';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';

/* Leaflet */
import L from 'leaflet';

/* Internal Components */
import Image from '../Image/Image';
import Map from '../Map/Map';

/* Utils */
import * as imageUtils from '../../utils/imageUtils';
import { NIS_HTML_ENTITY } from '../../utils/common';

/* Contexts */
import StoresContext from '../../contexts/storesContext';

// Paper style
const usePaperStyle = makeStyles({
    root: {
      height: '320px'
    }
  });

const ramiLeviStaticImages = (code) => `https://static.rami-levy.co.il/storage/images/${code}/medium.jpg`;

// Default location for map
const HAIFA_LOCATION = [32.7996897, 35.0517954];

const MAP_STYLE = { height: '550px' };

const Product = () => {
    // State
    const [product, setProduct] = useState({});
    const [productPrices, setProductPrices] = useState([]);
    const [loading, setloading] = useState(false);
    const [code, setCode] = useState('');
    const [invalidCode, setInvalidCode] = useState(false);
    const [markers, setMarkers] = useState([]);
    
    // Context
    const stores = useContext(StoresContext);
    //console.log('stores', stores);

    // Libs
    const { enqueueSnackbar } = useSnackbar();

    // Style
    const papersStyle = usePaperStyle();

    /**
     * Renderes the product in table
     * @param {any} product
     */
    const renderProduct = (product) => {
        // When product mot found
        if(!product) {
            return (<div>Product not found</div>);
        }

        // When product is empty (first render)
        if(!Object.keys(product).length) {
            return null;
        }

        // Render the product
        // 16000548909
        return (
            <div>
                <div style={{ width: '50%', float: 'left' }}>
                    <Image 
                        src={`http://localhost:49847/api/ImagesProxy/getProductImage?url=${ramiLeviStaticImages(code)}`}
                        imageStyle={{ width: '60%', height: 'auto' }} />
                </div>
                <div style={{ width: '50%', float: 'left', padding: '20px 20px 0 0' }}>
                    <Typography variant="h5" align="center" gutterBottom>
                        {product.name}
                    </Typography>
                    <Table aria-label="simple table">
                        <TableBody>
                            <TableRow>
                                <TableCell align="left">Manufacturer Name</TableCell>
                                <TableCell align="center">{product.manufacturerName}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell align="left">Manufacture Country</TableCell>
                                <TableCell align="center">{product.manufactureCountry}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell align="left">Manufacturer Description</TableCell>
                                <TableCell align="center">{product.manufacturerDescription}</TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </div>
            </div>
        );
    };

    /**
     * Called for every change and setting the code
     * @param {any} e
     */
    const handleCodeChange = (e) => {
        let code = e.target.value;
        code = (code && code.trim()) || '';

        const invalidCode = !code.length;

        setInvalidCode(invalidCode);
        setCode(code);
    };

    /**
     * Called when searching product
     * @param {any} e
     */
    const handleSearchClick = (e) => {
        const invalidCode = !!(!code || !code.length);

        if(invalidCode) {
            setInvalidCode(invalidCode);
        }
        else {
            getProductByID(code);
            getLastUpdatedProductPricesByID(code);
        }
    };

    /**
     * Gets product from api by code
     * @param {any} code
     */
    const getProductByID = async (code) => {
        setloading(true);
        
        let responseData = null;

        try {
            const response= await fetch('http://localhost:49847/api/products/getProductByCode/' + code);
            // console.log('response', response);
            responseData = await response.json();
        }
        catch(e) {
            console.log('e', e);
        }

        setloading(false);

        if (!responseData         ||
            !responseData.data    ||
            responseData.error) {
            enqueueSnackbar('product not found', { variant: 'warning' });

            setProduct(null);

            return;
        }
 
        setProduct(responseData.data);
    };

    /**
     * Gets last updated product prices from api by code
     * @param {any} code
     */
    const getLastUpdatedProductPricesByID = async (code) => {
        // setloading(true);
        setProduct([]);
        
        let responseData = null;

        try {
            const response= await fetch('http://localhost:49847/api/productPrices/getLastUpdatedProductPricesByCode/' + code);
            // console.log('response', response);
            responseData = await response.json();
        }
        catch(e) {
            console.log('e', e);
        }

        // setloading(false);

        if (!responseData         ||
            !responseData.data    ||
            responseData.error) {
            enqueueSnackbar('product not found', { variant: 'warning' });
            return;
        }
 
        setProductPrices(responseData.data.slice(0, 5));

        if(responseData.data.length) {
            let markersToAdd = [];
    
            responseData.data.forEach(productPrice => { 
                const { chainID = 0, storeID = '', price = 0 } = productPrice;
                const store = stores.find(s => s.internalChainID === chainID && s.storeID === storeID);
                const { location = {} } = store || {};
                const { coordinates = {} } = location;
                const { longitude = 0, latitude = 0 } = coordinates;
                
                if(longitude && latitude) {
                    markersToAdd.push(
                        L.marker([latitude, longitude], { 
                            icon: L.icon({ 
                                iconUrl: imageUtils.getChainMapIconByChainID(chainID), 
                                iconSize: [35, 35]
                            }) 
                        }).bindTooltip(`${price} ${NIS_HTML_ENTITY}`, { permanent: true })
                    );
                }
            });
    
            setMarkers(markersToAdd);
        }
    };

    /**
     * Renderes the product prices in table
     * @param {any} productPrices
     */
    const renderProductPrices = (productPrices) => {
        if(!productPrices || !productPrices.length) {
            return null;
        }

        return (
            <Table aria-label="simple table">
                <TableHead>
                <TableRow>
                    <TableCell align="center">Chain</TableCell>
                    <TableCell align="center">Sub Chain</TableCell>
                    <TableCell align="center">Store</TableCell>
                    <TableCell align="center">Price</TableCell>
                </TableRow>
                </TableHead>
                <TableBody>
                    { 
                        productPrices.map((productPrice, i) => { 
                            const { chainID = 0, storeID = '' } = productPrice;
                            const store = stores.find(s => s.internalChainID === chainID && s.storeID === storeID);
                            const { chainName = 'unknown', subChainName = 'unknown', storeName = 'unknown' } = store || {};

                            return (
                                <TableRow key={i}>
                                    <TableCell align="center">{chainName}</TableCell>
                                    <TableCell align="center">{subChainName}</TableCell>
                                    <TableCell align="center">{storeName}</TableCell>
                                    <TableCell align="center">{productPrice.price} &#8362;</TableCell>
                                </TableRow>
                            );
                        }) 
                    }
                </TableBody>
            </Table>
        );
    };
    
    return (
        <div>
            <TextField
                error={invalidCode}
                label={(invalidCode) ? "Invalid code" : "Code" }
                value={code} 
                onChange={handleCodeChange}
            />
            
            <Button variant="contained" color="primary" onClick={handleSearchClick}>search</Button>  

            <div style={{ margin: '50px 0 0 0' }}>
                <div style={{ width: '50%', float: 'left', paddingRight: '30px' }}>
                    <Paper elevation={3} classes={{ root: papersStyle.root }}>
                        { renderProductPrices(productPrices) }
                    </Paper>
                </div>
                <div style={{ width: '50%', float: 'left' }}>
                    <Paper elevation={3} classes={{ root: papersStyle.root }}>
                        { (loading) ? <CircularProgress /> : renderProduct(product) }
                    </Paper>
                </div>
                <div style={{ width: '100%', float: 'left', margin: '50px 0 0 0' }}>
                    <Paper elevation={3} classes={{ root: papersStyle.root }}>
                        <div className="mapWarpper">
                            <Map    
                                initialView={HAIFA_LOCATION} 
                                markers={markers} 
                                style={MAP_STYLE} />
                        </div>
                    </Paper>
                </div>
            </div>
        </div>
    );
};


export default Product;