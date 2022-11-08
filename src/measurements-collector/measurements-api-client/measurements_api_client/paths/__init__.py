# do not import all endpoints into this module because that uses a lot of memory and stack frames
# if you need the ability to import all endpoints from this module, import them with
# from measurements_api_client.apis.path_to_api import path_to_api

import enum


class PathValues(str, enum.Enum):
    MEASUREMENTS = "/measurements"
    MEASUREMENTS_BATCHINSERT = "/measurements/batch-insert"
    SENSORS = "/sensors"
    SENSORS_ID = "/sensors/{id}"
