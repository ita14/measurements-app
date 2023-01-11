import React from 'react';
import App from './features/app/App';
import { BrowserRouter } from 'react-router-dom';
import './index.css';
import { createRoot } from 'react-dom/client';
import { StyledEngineProvider } from '@mui/material/styles';
import { ReactKeycloakProvider } from '@react-keycloak/web';
import keycloak from './keycloak';

const container = document.getElementById('root');
// eslint-disable-next-line @typescript-eslint/no-non-null-assertion
const root = createRoot(container!);

// Keep KeycloakProvider outside strict mode as it causes infinite loop.
root.render(
  <ReactKeycloakProvider authClient={keycloak}>
    <React.StrictMode>
      <StyledEngineProvider injectFirst>
        <BrowserRouter>
          <App />
        </BrowserRouter>
      </StyledEngineProvider>
    </React.StrictMode>
  </ReactKeycloakProvider>
);
