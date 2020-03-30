/* React */
import React, { useState, useEffect } from 'react';
import { Route } from 'react-router';

/* Internal Components */
import Dashboared from './components/Dashboared/Dashboared';
import Product from './components/Product/Product';
import PersistentDrawerLeft from './components/PersistentDrawerLeft/PersistentDrawerLeft';
import Sandbox from './components/Sandbox/Sandbox';

/* Contexts */
import { StoresProvider } from './contexts/storesContext';

/* Material UI */
import { SnackbarProvider } from 'notistack';

/* API */
import { getStores } from './api/store';

/* CSS */
import './custom.css'

const App = () => {
  const [stores, setStores] = useState([]);

  useEffect(() => {
    loadStores();
  }, []);

  const loadStores = async () => {
    const responseData = await getStores();

    setStores(responseData.data);
  };

  return (
      <StoresProvider value={stores}>
        <SnackbarProvider maxSnack={1} anchorOrigin={{ horizontal: 'center', vertical: 'bottom' }}>
          <PersistentDrawerLeft>
              <Route exact path='/' component={Dashboared} />
              <Route path='/products/:code' component={Product} />
              <Route path='/sandbox' component={Sandbox} />
          </PersistentDrawerLeft>
        </SnackbarProvider>
      </StoresProvider>
  );
};

export default App;