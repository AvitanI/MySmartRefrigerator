import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Product } from './components/Product';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/findProduct' component={Product} />
      </Layout>
    );
  }
}
