import React, { Component } from 'react';
import { Route } from 'react-router';
import { Dasboared } from './components/Dasboared';
import { Product } from './components/Product';
import PersistentDrawerLeft from './components/PersistentDrawerLeft/PersistentDrawerLeft';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <PersistentDrawerLeft>
            <Route exact path='/' component={Dasboared} />
            <Route path='/findProduct' component={Product} />
        </PersistentDrawerLeft>
    );
  }
}
