<a name="__pageTop"></a>
# measurements_api_client.apis.tags.sensors_api.SensorsApi

All URIs are relative to *http://localhost:7001*

Method | HTTP request | Description
------------- | ------------- | -------------
[**delete_sensor**](#delete_sensor) | **delete** /sensors/{id} | 
[**get_sensor_by_id**](#get_sensor_by_id) | **get** /sensors/{id} | 
[**get_sensors**](#get_sensors) | **get** /sensors | 
[**post_sensor**](#post_sensor) | **post** /sensors | 
[**put_sensor**](#put_sensor) | **put** /sensors/{id} | 

# **delete_sensor**
<a name="delete_sensor"></a>
> delete_sensor(id)



Delete sensor

### Example

```python
import measurements_api_client
from measurements_api_client.apis.tags import sensors_api
from measurements_api_client.model.problem_details import ProblemDetails
from pprint import pprint
# Defining the host is optional and defaults to http://localhost:7001
# See configuration.py for a list of all supported configuration parameters.
configuration = measurements_api_client.Configuration(
    host = "http://localhost:7001"
)

# Enter a context with an instance of the API client
with measurements_api_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = sensors_api.SensorsApi(api_client)

    # example passing only required values which don't have defaults set
    path_params = {
        'id': "id_example",
    }
    try:
        api_response = api_instance.delete_sensor(
            path_params=path_params,
        )
    except measurements_api_client.ApiException as e:
        print("Exception when calling SensorsApi->delete_sensor: %s\n" % e)
```
### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
path_params | RequestPathParams | |
accept_content_types | typing.Tuple[str] | default is ('application/json; charset&#x3D;utf-8', ) | Tells the server the content type(s) that are accepted by the client
stream | bool | default is False | if True then the response.content will be streamed and loaded from a file like object. When downloading a file, set this to True to force the code to deserialize the content to a FileSchema file
timeout | typing.Optional[typing.Union[int, typing.Tuple]] | default is None | the timeout used by the rest client
skip_deserialization | bool | default is False | when True, headers and body will be unset and an instance of api_client.ApiResponseWithoutDeserialization will be returned

### path_params
#### RequestPathParams

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
id | IdSchema | | 

# IdSchema

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
str,  | str,  |  | 

### Return Types, Responses

Code | Class | Description
------------- | ------------- | -------------
n/a | api_client.ApiResponseWithoutDeserialization | When skip_deserialization is True this response is returned
204 | [ApiResponseFor204](#delete_sensor.ApiResponseFor204) | Sensor deleted
404 | [ApiResponseFor404](#delete_sensor.ApiResponseFor404) | Sensor not found
default | [ApiResponseForDefault](#delete_sensor.ApiResponseForDefault) | Unexpected error

#### delete_sensor.ApiResponseFor204
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | Unset | body was not defined |
headers | Unset | headers were not defined |

#### delete_sensor.ApiResponseFor404
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | Unset | body was not defined |
headers | Unset | headers were not defined |

#### delete_sensor.ApiResponseForDefault
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | typing.Union[SchemaFor0ResponseBodyApplicationJsonCharsetutf8, ] |  |
headers | Unset | headers were not defined |

# SchemaFor0ResponseBodyApplicationJsonCharsetutf8
Type | Description  | Notes
------------- | ------------- | -------------
[**ProblemDetails**](../../models/ProblemDetails.md) |  | 


### Authorization

No authorization required

[[Back to top]](#__pageTop) [[Back to API list]](../../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../../README.md#documentation-for-models) [[Back to README]](../../../README.md)

# **get_sensor_by_id**
<a name="get_sensor_by_id"></a>
> Sensor get_sensor_by_id(id)



Get sensor by id

### Example

```python
import measurements_api_client
from measurements_api_client.apis.tags import sensors_api
from measurements_api_client.model.sensor import Sensor
from measurements_api_client.model.problem_details import ProblemDetails
from pprint import pprint
# Defining the host is optional and defaults to http://localhost:7001
# See configuration.py for a list of all supported configuration parameters.
configuration = measurements_api_client.Configuration(
    host = "http://localhost:7001"
)

# Enter a context with an instance of the API client
with measurements_api_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = sensors_api.SensorsApi(api_client)

    # example passing only required values which don't have defaults set
    path_params = {
        'id': "id_example",
    }
    try:
        api_response = api_instance.get_sensor_by_id(
            path_params=path_params,
        )
        pprint(api_response)
    except measurements_api_client.ApiException as e:
        print("Exception when calling SensorsApi->get_sensor_by_id: %s\n" % e)
```
### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
path_params | RequestPathParams | |
accept_content_types | typing.Tuple[str] | default is ('application/json', 'application/json; charset&#x3D;utf-8', ) | Tells the server the content type(s) that are accepted by the client
stream | bool | default is False | if True then the response.content will be streamed and loaded from a file like object. When downloading a file, set this to True to force the code to deserialize the content to a FileSchema file
timeout | typing.Optional[typing.Union[int, typing.Tuple]] | default is None | the timeout used by the rest client
skip_deserialization | bool | default is False | when True, headers and body will be unset and an instance of api_client.ApiResponseWithoutDeserialization will be returned

### path_params
#### RequestPathParams

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
id | IdSchema | | 

# IdSchema

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
str,  | str,  |  | 

### Return Types, Responses

Code | Class | Description
------------- | ------------- | -------------
n/a | api_client.ApiResponseWithoutDeserialization | When skip_deserialization is True this response is returned
200 | [ApiResponseFor200](#get_sensor_by_id.ApiResponseFor200) | Sensor response
404 | [ApiResponseFor404](#get_sensor_by_id.ApiResponseFor404) | Sensor not found
default | [ApiResponseForDefault](#get_sensor_by_id.ApiResponseForDefault) | Unexpected error

#### get_sensor_by_id.ApiResponseFor200
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | typing.Union[SchemaFor200ResponseBodyApplicationJson, ] |  |
headers | Unset | headers were not defined |

# SchemaFor200ResponseBodyApplicationJson
Type | Description  | Notes
------------- | ------------- | -------------
[**Sensor**](../../models/Sensor.md) |  | 


#### get_sensor_by_id.ApiResponseFor404
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | Unset | body was not defined |
headers | Unset | headers were not defined |

#### get_sensor_by_id.ApiResponseForDefault
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | typing.Union[SchemaFor0ResponseBodyApplicationJsonCharsetutf8, ] |  |
headers | Unset | headers were not defined |

# SchemaFor0ResponseBodyApplicationJsonCharsetutf8
Type | Description  | Notes
------------- | ------------- | -------------
[**ProblemDetails**](../../models/ProblemDetails.md) |  | 


### Authorization

No authorization required

[[Back to top]](#__pageTop) [[Back to API list]](../../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../../README.md#documentation-for-models) [[Back to README]](../../../README.md)

# **get_sensors**
<a name="get_sensors"></a>
> [Sensor] get_sensors()



Get sensors.

### Example

```python
import measurements_api_client
from measurements_api_client.apis.tags import sensors_api
from measurements_api_client.model.sensor import Sensor
from pprint import pprint
# Defining the host is optional and defaults to http://localhost:7001
# See configuration.py for a list of all supported configuration parameters.
configuration = measurements_api_client.Configuration(
    host = "http://localhost:7001"
)

# Enter a context with an instance of the API client
with measurements_api_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = sensors_api.SensorsApi(api_client)

    # example, this endpoint has no required or optional parameters
    try:
        api_response = api_instance.get_sensors()
        pprint(api_response)
    except measurements_api_client.ApiException as e:
        print("Exception when calling SensorsApi->get_sensors: %s\n" % e)
```
### Parameters
This endpoint does not need any parameter.

### Return Types, Responses

Code | Class | Description
------------- | ------------- | -------------
n/a | api_client.ApiResponseWithoutDeserialization | When skip_deserialization is True this response is returned
200 | [ApiResponseFor200](#get_sensors.ApiResponseFor200) | Sensors response

#### get_sensors.ApiResponseFor200
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | typing.Union[SchemaFor200ResponseBodyApplicationJson, ] |  |
headers | Unset | headers were not defined |

# SchemaFor200ResponseBodyApplicationJson

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
list, tuple,  | tuple,  |  | 

### Tuple Items
Class Name | Input Type | Accessed Type | Description | Notes
------------- | ------------- | ------------- | ------------- | -------------
[**Sensor**]({{complexTypePrefix}}Sensor.md) | [**Sensor**]({{complexTypePrefix}}Sensor.md) | [**Sensor**]({{complexTypePrefix}}Sensor.md) |  | 

### Authorization

No authorization required

[[Back to top]](#__pageTop) [[Back to API list]](../../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../../README.md#documentation-for-models) [[Back to README]](../../../README.md)

# **post_sensor**
<a name="post_sensor"></a>
> Sensor post_sensor(sensor)



Create sensor.

### Example

```python
import measurements_api_client
from measurements_api_client.apis.tags import sensors_api
from measurements_api_client.model.sensor import Sensor
from measurements_api_client.model.problem_details import ProblemDetails
from pprint import pprint
# Defining the host is optional and defaults to http://localhost:7001
# See configuration.py for a list of all supported configuration parameters.
configuration = measurements_api_client.Configuration(
    host = "http://localhost:7001"
)

# Enter a context with an instance of the API client
with measurements_api_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = sensors_api.SensorsApi(api_client)

    # example passing only required values which don't have defaults set
    body = Sensor(
        id="id_example",
        description="description_example",
    )
    try:
        api_response = api_instance.post_sensor(
            body=body,
        )
        pprint(api_response)
    except measurements_api_client.ApiException as e:
        print("Exception when calling SensorsApi->post_sensor: %s\n" % e)
```
### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
body | typing.Union[SchemaForRequestBodyApplicationJson] | required |
content_type | str | optional, default is 'application/json' | Selects the schema and serialization of the request body
accept_content_types | typing.Tuple[str] | default is ('application/json', 'application/json; charset&#x3D;utf-8', ) | Tells the server the content type(s) that are accepted by the client
stream | bool | default is False | if True then the response.content will be streamed and loaded from a file like object. When downloading a file, set this to True to force the code to deserialize the content to a FileSchema file
timeout | typing.Optional[typing.Union[int, typing.Tuple]] | default is None | the timeout used by the rest client
skip_deserialization | bool | default is False | when True, headers and body will be unset and an instance of api_client.ApiResponseWithoutDeserialization will be returned

### body

# SchemaForRequestBodyApplicationJson
Type | Description  | Notes
------------- | ------------- | -------------
[**Sensor**](../../models/Sensor.md) |  | 


### Return Types, Responses

Code | Class | Description
------------- | ------------- | -------------
n/a | api_client.ApiResponseWithoutDeserialization | When skip_deserialization is True this response is returned
200 | [ApiResponseFor200](#post_sensor.ApiResponseFor200) | Sensor response
default | [ApiResponseForDefault](#post_sensor.ApiResponseForDefault) | Unexpected error

#### post_sensor.ApiResponseFor200
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | typing.Union[SchemaFor200ResponseBodyApplicationJson, ] |  |
headers | Unset | headers were not defined |

# SchemaFor200ResponseBodyApplicationJson
Type | Description  | Notes
------------- | ------------- | -------------
[**Sensor**](../../models/Sensor.md) |  | 


#### post_sensor.ApiResponseForDefault
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | typing.Union[SchemaFor0ResponseBodyApplicationJsonCharsetutf8, ] |  |
headers | Unset | headers were not defined |

# SchemaFor0ResponseBodyApplicationJsonCharsetutf8
Type | Description  | Notes
------------- | ------------- | -------------
[**ProblemDetails**](../../models/ProblemDetails.md) |  | 


### Authorization

No authorization required

[[Back to top]](#__pageTop) [[Back to API list]](../../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../../README.md#documentation-for-models) [[Back to README]](../../../README.md)

# **put_sensor**
<a name="put_sensor"></a>
> put_sensor(idsensor)



Update sensor

### Example

```python
import measurements_api_client
from measurements_api_client.apis.tags import sensors_api
from measurements_api_client.model.sensor import Sensor
from measurements_api_client.model.problem_details import ProblemDetails
from pprint import pprint
# Defining the host is optional and defaults to http://localhost:7001
# See configuration.py for a list of all supported configuration parameters.
configuration = measurements_api_client.Configuration(
    host = "http://localhost:7001"
)

# Enter a context with an instance of the API client
with measurements_api_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = sensors_api.SensorsApi(api_client)

    # example passing only required values which don't have defaults set
    path_params = {
        'id': "id_example",
    }
    body = Sensor(
        id="id_example",
        description="description_example",
    )
    try:
        api_response = api_instance.put_sensor(
            path_params=path_params,
            body=body,
        )
    except measurements_api_client.ApiException as e:
        print("Exception when calling SensorsApi->put_sensor: %s\n" % e)
```
### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
body | typing.Union[SchemaForRequestBodyApplicationJson] | required |
path_params | RequestPathParams | |
content_type | str | optional, default is 'application/json' | Selects the schema and serialization of the request body
accept_content_types | typing.Tuple[str] | default is ('application/json; charset&#x3D;utf-8', ) | Tells the server the content type(s) that are accepted by the client
stream | bool | default is False | if True then the response.content will be streamed and loaded from a file like object. When downloading a file, set this to True to force the code to deserialize the content to a FileSchema file
timeout | typing.Optional[typing.Union[int, typing.Tuple]] | default is None | the timeout used by the rest client
skip_deserialization | bool | default is False | when True, headers and body will be unset and an instance of api_client.ApiResponseWithoutDeserialization will be returned

### body

# SchemaForRequestBodyApplicationJson
Type | Description  | Notes
------------- | ------------- | -------------
[**Sensor**](../../models/Sensor.md) |  | 


### path_params
#### RequestPathParams

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
id | IdSchema | | 

# IdSchema

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
str,  | str,  |  | 

### Return Types, Responses

Code | Class | Description
------------- | ------------- | -------------
n/a | api_client.ApiResponseWithoutDeserialization | When skip_deserialization is True this response is returned
204 | [ApiResponseFor204](#put_sensor.ApiResponseFor204) | Sensor updated
404 | [ApiResponseFor404](#put_sensor.ApiResponseFor404) | Sensor not found
default | [ApiResponseForDefault](#put_sensor.ApiResponseForDefault) | Unexpected error

#### put_sensor.ApiResponseFor204
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | Unset | body was not defined |
headers | Unset | headers were not defined |

#### put_sensor.ApiResponseFor404
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | Unset | body was not defined |
headers | Unset | headers were not defined |

#### put_sensor.ApiResponseForDefault
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | typing.Union[SchemaFor0ResponseBodyApplicationJsonCharsetutf8, ] |  |
headers | Unset | headers were not defined |

# SchemaFor0ResponseBodyApplicationJsonCharsetutf8
Type | Description  | Notes
------------- | ------------- | -------------
[**ProblemDetails**](../../models/ProblemDetails.md) |  | 


### Authorization

No authorization required

[[Back to top]](#__pageTop) [[Back to API list]](../../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../../README.md#documentation-for-models) [[Back to README]](../../../README.md)

