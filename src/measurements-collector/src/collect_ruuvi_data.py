"""
Collect ruuvitag sensor data and upload to measurements api
"""

import time
from datetime import datetime, timezone
from random import uniform
from ruuvitag_sensor.ruuvi import RuuviTagSensor
from measurements_api_client.model.measurement import Measurement
from measurements_api_client.model.acceleration import Acceleration
from measurements_api_client import ApiException
from api_publisher import publish_measurements
from logger import logger
import config


def collect() -> list[Measurement]:
    logger.info(f'Collecting at {datetime.utcnow()}')

    results = []
    timeout_in_sec = 4

    if config.use_mock_data:
        logger.info('Using mock data')
        data = get_mock_data(config.macs)
    else:
        logger.info('Reading ruuvitag data...')
        data = RuuviTagSensor.get_data_for_sensors(config.macs, timeout_in_sec)

    time_now = datetime.now(timezone.utc).isoformat()

    for key, value in data.items():
        results.append(Measurement(
            time=time_now,
            source=key,
            temperature=round(value['temperature'], 2),
            pressure=value['pressure'],
            humidity=value['humidity'],
            battery=value['battery'],
            acceleration=Acceleration(
                acceleration=value['acceleration'],
                accelerationX=value['acceleration_x'],
                accelerationY=value['acceleration_y'],
                accelerationZ=value['acceleration_z'],
            )
        ))

    return results


def get_mock_data(macs: list):
    measurements = {}

    for mac in macs:
        measurements[mac] = {
            'data_format':    5,
            'temperature':    uniform(15, 22),
            'humidity':       uniform(15, 50),
            'pressure':       uniform(990, 1020),
            'acceleration':   uniform(990, 1020),
            'acceleration_y': uniform(-100, 100),
            'acceleration_x': uniform(-100, 100),
            'acceleration_z': uniform(990, 1020),
            'battery':        uniform(2500, 3000)
        }
    return measurements


def main():
    while 1:
        try:
            measurements = collect()
            publish_measurements(measurements)
        except ApiException as e:
            logger.error(f'Exception when publishing measurements: {e}')
        except Exception as e:
            # Don't exit
            logger.error(f'Failed to process messages: {e}')
        finally:
            time.sleep(config.interval_seconds)


if __name__ == "__main__":
    main()
