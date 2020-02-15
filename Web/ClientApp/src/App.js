import React, { Component } from 'react';
import { Route } from 'react-router';
//import { Dasboared } from './components/Dasboared';
import Product from './components/Product';
import PersistentDrawerLeft from './components/PersistentDrawerLeft/PersistentDrawerLeft';
import { SnackbarProvider } from 'notistack';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
      return (
          <SnackbarProvider maxSnack={1} anchorOrigin={{ horizontal: 'center', vertical: 'bottom' }}>
            <PersistentDrawerLeft>
                <Route exact path='/' component={Product /*Dasboared*/} />
                <Route path='/findProduct' component={Product} />
            </PersistentDrawerLeft>
          </SnackbarProvider>
    );
  }
}
