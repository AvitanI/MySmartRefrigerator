import React, { Component } from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import { withSnackbar } from 'notistack';
import CircularProgress from '@material-ui/core/CircularProgress';

export class Product extends Component {
    
    constructor(props) {
        super(props);

        this.state = {
            product: {},
            loading: false,
            code: '',
            invalidCode: false
        };

        this.handleCodeChange = this.handleCodeChange.bind(this);
        this.handleSearchClick = this.handleSearchClick.bind(this);
    }

    /* Class Methods */

    /**
     * Renderes the product in table
     * @param {any} product
     */
    static renderProduct(product) {
        return (
        <table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
            <tr>
                <th>ID</th>
                <th>Code</th>
                <th>Name</th>
                <th>Last Update</th>
            </tr>
            </thead>
            <tbody>
                <tr>
                    <td>{product.id}</td>
                    <td>{product.code}</td>
                    <td>{product.name}</td>
                    <td>{product.lastUpdate}</td>
                </tr>
            </tbody>
        </table>
        );
  }

    /* Instance Methods */

    /**
     * Called for every change and setting the code
     * @param {any} e
     */
    handleCodeChange(e) {
        let code = e.target.value;
        code = (code && code.trim()) || '';

        const invalidCode = !code.length;

        this.setState({ code, invalidCode });
    }

    /**
     * Called when searching product
     * @param {any} e
     */
    handleSearchClick(e) {
        const { code } = this.state;
        const invalidCode = !!(!code || !code.length);

        if(invalidCode) {
            this.setState({ invalidCode });
        }
        else {
            this.getProductByID(this.state.code);
        }
    }

    /**
     * gets product from api by code
     * @param {any} code
     */
    async getProductByID(code) {
        this.setState({ loading: true, product: {} });

        let responseData = null;

        try {
            const response= await fetch('http://localhost:49847/api/products/getProductByCode/' + code);
            console.log('response', response);
            responseData = await response.json();
        }
        catch(e) {
            console.log('e', e);
        }

        this.setState({ loading: false });

        if (!responseData         ||
            !responseData.data    ||
            responseData.error) {
            this.props.enqueueSnackbar('product not found', { variant: 'warning' });
            return;
        }
 
        this.setState({ product: responseData.data });
    }

    render() {    
        const { loading, product, code, invalidCode } = this.state;
        
        return (
            <div>
                <h1 id="tabelLabel">Product Info</h1>
                <br />
                <TextField
                    error={invalidCode}
                    label={(invalidCode) ? "Invalid code" : "Code" }
                    value={code} 
                    onChange={this.handleCodeChange}
                />
                
                <Button variant="contained" color="primary" onClick={this.handleSearchClick}>search</Button>  

                <div style={{ margin: '50px 0 0 0' }}>
                    { (loading) ? <CircularProgress /> : Product.renderProduct(product) }
                </div>
            </div>
        );
    }
}

export default withSnackbar(Product);