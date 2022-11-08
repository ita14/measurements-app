from measurements_api_client.paths.sensors_id.get import ApiForget
from measurements_api_client.paths.sensors_id.put import ApiForput
from measurements_api_client.paths.sensors_id.delete import ApiFordelete


class SensorsId(
    ApiForget,
    ApiForput,
    ApiFordelete,
):
    pass
