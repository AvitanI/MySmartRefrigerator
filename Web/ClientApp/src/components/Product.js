import React, { Component } from 'react';

export class Product extends Component {
  static displayName = Product.name;

  constructor(props) {
    super(props);

    this.state = { product: [], loading: true };
  }

  componentDidMount() {
    this.getProductByID();
  }

    static renderProduct(product) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>id</th>
            <th>name</th>
          </tr>
        </thead>
        <tbody>
            <tr>
                <td>{product.id}</td>
                <td>{product.name}</td>
            </tr>
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : Product.renderProduct(this.state.product);

    return (
      <div>
        <h1 id="tabelLabel">Product Info</h1>
        {contents}
      </div>
    );
  }

  async getProductByID() {
    const response = await fetch('http://localhost:49847/api/products/getProductByCode/11210000094');
    const data = await response.json();
      this.setState({ product: data, loading: false });
  }
}
