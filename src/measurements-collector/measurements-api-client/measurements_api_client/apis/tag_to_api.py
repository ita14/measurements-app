import typing_extensions

from measurements_api_client.apis.tags import TagValues
from measurements_api_client.apis.tags.measurements_api import MeasurementsApi
from measurements_api_client.apis.tags.sensors_api import SensorsApi

TagToApi = typing_extensions.TypedDict(
    'TagToApi',
    {
        TagValues.MEASUREMENTS: MeasurementsApi,
        TagValues.SENSORS: SensorsApi,
    }
)

tag_to_api = TagToApi(
    {
        TagValues.MEASUREMENTS: MeasurementsApi,
        TagValues.SENSORS: SensorsApi,
    }
)
