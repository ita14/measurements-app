import typing_extensions

from measurements_api_client.paths import PathValues
from measurements_api_client.apis.paths.measurements import Measurements
from measurements_api_client.apis.paths.measurements_batch_insert import MeasurementsBatchInsert
from measurements_api_client.apis.paths.sensors import Sensors
from measurements_api_client.apis.paths.sensors_id import SensorsId

PathToApi = typing_extensions.TypedDict(
    'PathToApi',
    {
        PathValues.MEASUREMENTS: Measurements,
        PathValues.MEASUREMENTS_BATCHINSERT: MeasurementsBatchInsert,
        PathValues.SENSORS: Sensors,
        PathValues.SENSORS_ID: SensorsId,
    }
)

path_to_api = PathToApi(
    {
        PathValues.MEASUREMENTS: Measurements,
        PathValues.MEASUREMENTS_BATCHINSERT: MeasurementsBatchInsert,
        PathValues.SENSORS: Sensors,
        PathValues.SENSORS_ID: SensorsId,
    }
)
