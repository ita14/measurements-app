"""
Keycloak
"""

from keycloak import KeycloakOpenID
import config


def get_token():
    keycloak_openid = KeycloakOpenID(server_url=config.keycloak_url,
                                     client_id=config.client_id,
                                     realm_name=config.realm,
                                     client_secret_key=config.client_secret)

    return keycloak_openid.token(grant_type=['client_credentials'])['access_token']
