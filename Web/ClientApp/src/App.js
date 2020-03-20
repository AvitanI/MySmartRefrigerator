import React, { useState, useEffect } from 'react';
import { Route } from 'react-router';
//import { Dasboared } from './components/Dasboared/Dasboared';
import Product from './components/Product/Product';
import PersistentDrawerLeft from './components/PersistentDrawerLeft/PersistentDrawerLeft';
import { SnackbarProvider } from 'notistack';
import { StoresProvider } from './contexts/storesContext';

import './custom.css'

const App = () => {
  const [stores, setStores] = useState([]);

  useEffect(() => {
    loadStores();
  }, []);

  const loadStores = async () => {
    try {
      const response = await fetch('http://localhost:49847/api/stores/getStores');
	  const responseData = await response.json();
	  
	  setStores(responseData.data);
    }
    catch(e) {
        console.log('e', e);
    }
  };

  return (
      <StoresProvider value={stores}>
        <SnackbarProvider maxSnack={1} anchorOrigin={{ horizontal: 'center', vertical: 'bottom' }}>
          <PersistentDrawerLeft>
              <Route exact path='/' component={Product /*Dasboared*/} />
              <Route path='/findProduct' component={Product} />
          </PersistentDrawerLeft>
        </SnackbarProvider>
      </StoresProvider>
  );
};

export default App;