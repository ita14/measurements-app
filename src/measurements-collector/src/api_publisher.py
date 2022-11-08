"""
Publish measurements
"""

import measurements_api_client
from measurements_api_client.apis.tags import measurements_api
from measurements_api_client.model.measurement import Measurement
from config import api_endpoint
from logger import logger


configuration = measurements_api_client.Configuration(
    host=api_endpoint
)


def publish_measurements(measurements: list[Measurement]):
    with measurements_api_client.ApiClient(configuration) as api_client:
        api_instance = measurements_api.MeasurementsApi(api_client)

        logger.info(f'Publishing {len(measurements)} measurements...')

        api_instance.post_measurements(
            body=measurements
        )
