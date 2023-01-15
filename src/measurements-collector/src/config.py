"""
Configuration for local dev env
"""

api_endpoint = 'measurements-api.local'
use_mock_data = True
interval_seconds = 30
macs = [
    'F3:D9:E8:28:41:5B',
    'C6:4C:96:B3:20:7E',
    'D8:4E:92:B8:86:22',
    'EA:04:8D:66:BE:87'
]

# Keycloak authentication (client credentials flow).
token_endpoint = 'http://keycloak.local:8080/realms/measurements-app/protocol/openid-connect/token'
client_id = 'measurements-collector'
client_secret = 'Pb4pMgu8ZB8pJEJViRoRqKcSxXj4o0Hr'
realm = 'measurements-app'
keycloak_url = 'http://keycloak.local:8080'
