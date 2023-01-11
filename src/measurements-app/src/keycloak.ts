import Keycloak from 'keycloak-js';

// TODO: url from app env
const keycloak = new Keycloak({
  url: 'http://localhost:8080',
  realm: 'measurements-app',
  clientId: 'measurements-webapp'
});

export default keycloak;
