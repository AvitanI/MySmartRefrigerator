import React, { Component } from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import { withSnackbar } from 'notistack';

export class Product extends Component {
    // Class variables
    static displayName = Product.name;

    constructor(props) {
        super(props);

        this.state = {
            product: {
                id: '',
                code: '',
                name: '',
                lastUpdate: ''
            },
            loading: true,
            code: ''
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
        this.setState({ code: e.target.value });
    }

    /**
     * Called when searching product
     * @param {any} e
     */
    handleSearchClick(e) {
        this.getProductByID(this.state.code);
    }

    /**
     * gets product from api by code
     * @param {any} code
     */
    async getProductByID(code) {
        const response = await fetch('http://localhost:49847/api/products/getProductByCode/' + code);
        const responseData = await response.json();

        if (!responseData         ||
            !responseData.data    ||
            responseData.error) {
            this.props.enqueueSnackbar('product not found', { variant: 'warning' });
            console.log('response', response);
            return;
        }
 
        this.setState({ product: responseData.data, loading: false });
    }

    render() {
    
    return (
        <div>
            <h1 id="tabelLabel">Product Info</h1>
            <br />
            <TextField id="standard-basic" label="Code" value={this.state.code} onChange={this.handleCodeChange} />
            <Button variant="contained" color="primary" onClick={this.handleSearchClick}>search</Button>
            {Product.renderProduct(this.state.product)}
        </div>
    );
    }
}

export default withSnackbar(Product);