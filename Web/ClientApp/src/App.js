import React from 'react';
import { Route } from 'react-router';
//import { Dasboared } from './components/Dasboared/Dasboared';
import Product from './components/Product/Product';
import PersistentDrawerLeft from './components/PersistentDrawerLeft/PersistentDrawerLeft';
import { SnackbarProvider } from 'notistack';

import './custom.css'

const App = () => {
  return (
      <SnackbarProvider maxSnack={1} anchorOrigin={{ horizontal: 'center', vertical: 'bottom' }}>
        <PersistentDrawerLeft>
            <Route exact path='/' component={Product /*Dasboared*/} />
            <Route path='/findProduct' component={Product} />
        </PersistentDrawerLeft>
      </SnackbarProvider>
  );
};

export default App;