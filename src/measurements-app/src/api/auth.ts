import { Configuration, ConfigurationParameters } from '../generated/measurements-api-client';
import keycloak from '../keycloak';

const MIN_VALIDITY_SEC = 10;

export const configurationParams: ConfigurationParameters = {
  accessToken: getAccessToken
};

export const apiConfig = new Configuration({
  accessToken: getAccessToken
});

async function getAccessToken(name?: string, scopes?: string[]): Promise<string> {
  if (keycloak.isTokenExpired()) {
    await keycloak.updateToken(MIN_VALIDITY_SEC);
  }

  return keycloak.token ?? '';
}
