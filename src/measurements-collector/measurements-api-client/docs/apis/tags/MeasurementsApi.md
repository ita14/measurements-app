<a name="__pageTop"></a>
# measurements_api_client.apis.tags.measurements_api.MeasurementsApi

All URIs are relative to *http://localhost:7001*

Method | HTTP request | Description
------------- | ------------- | -------------
[**get_measurements**](#get_measurements) | **get** /measurements | 
[**post_measurements**](#post_measurements) | **post** /measurements/batch-insert | 

# **get_measurements**
<a name="get_measurements"></a>
> MeasurementsDataResponse get_measurements()



Return measurement data for selected time period.

### Example

```python
import measurements_api_client
from measurements_api_client.apis.tags import measurements_api
from measurements_api_client.model.validation_problem_details import ValidationProblemDetails
from measurements_api_client.model.problem_details import ProblemDetails
from measurements_api_client.model.measurements_data_response import MeasurementsDataResponse
from pprint import pprint
# Defining the host is optional and defaults to http://localhost:7001
# See configuration.py for a list of all supported configuration parameters.
configuration = measurements_api_client.Configuration(
    host = "http://localhost:7001"
)

# Enter a context with an instance of the API client
with measurements_api_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = measurements_api.MeasurementsApi(api_client)

    # example passing only optional values
    query_params = {
        'startTime': "2022-06-21T17:32:28Z",
        'endTime': "2022-06-22T17:32:28Z",
        'source': "C6:4C:96:B3:20:7E",
        'orderBy': "time:asc",
        'limit': 100,
        'offset': 0,
    }
    try:
        api_response = api_instance.get_measurements(
            query_params=query_params,
        )
        pprint(api_response)
    except measurements_api_client.ApiException as e:
        print("Exception when calling MeasurementsApi->get_measurements: %s\n" % e)
```
### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
query_params | RequestQueryParams | |
accept_content_types | typing.Tuple[str] | default is ('application/json; charset&#x3D;utf-8', ) | Tells the server the content type(s) that are accepted by the client
stream | bool | default is False | if True then the response.content will be streamed and loaded from a file like object. When downloading a file, set this to True to force the code to deserialize the content to a FileSchema file
timeout | typing.Optional[typing.Union[int, typing.Tuple]] | default is None | the timeout used by the rest client
skip_deserialization | bool | default is False | when True, headers and body will be unset and an instance of api_client.ApiResponseWithoutDeserialization will be returned

### query_params
#### RequestQueryParams

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
startTime | StartTimeSchema | | optional
endTime | EndTimeSchema | | optional
source | SourceSchema | | optional
orderBy | OrderBySchema | | optional
limit | LimitSchema | | optional
offset | OffsetSchema | | optional


# StartTimeSchema

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
None, str, datetime,  | NoneClass, str,  |  | value must conform to RFC-3339 date-time

# EndTimeSchema

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
None, str, datetime,  | NoneClass, str,  |  | value must conform to RFC-3339 date-time

# SourceSchema

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
None, str,  | NoneClass, str,  |  | 

# OrderBySchema

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
str,  | str,  |  | if omitted the server will use the default value of "time:asc"

# LimitSchema

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
decimal.Decimal, int,  | decimal.Decimal,  |  | if omitted the server will use the default value of 100

# OffsetSchema

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
decimal.Decimal, int,  | decimal.Decimal,  |  | if omitted the server will use the default value of 0

### Return Types, Responses

Code | Class | Description
------------- | ------------- | -------------
n/a | api_client.ApiResponseWithoutDeserialization | When skip_deserialization is True this response is returned
200 | [ApiResponseFor200](#get_measurements.ApiResponseFor200) | Measurements response
400 | [ApiResponseFor400](#get_measurements.ApiResponseFor400) | Bad Request
default | [ApiResponseForDefault](#get_measurements.ApiResponseForDefault) | Unexpected error

#### get_measurements.ApiResponseFor200
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | typing.Union[SchemaFor200ResponseBodyApplicationJsonCharsetutf8, ] |  |
headers | Unset | headers were not defined |

# SchemaFor200ResponseBodyApplicationJsonCharsetutf8
Type | Description  | Notes
------------- | ------------- | -------------
[**MeasurementsDataResponse**](../../models/MeasurementsDataResponse.md) |  | 


#### get_measurements.ApiResponseFor400
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | typing.Union[SchemaFor400ResponseBodyApplicationJsonCharsetutf8, ] |  |
headers | Unset | headers were not defined |

# SchemaFor400ResponseBodyApplicationJsonCharsetutf8
Type | Description  | Notes
------------- | ------------- | -------------
[**ValidationProblemDetails**](../../models/ValidationProblemDetails.md) |  | 


#### get_measurements.ApiResponseForDefault
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

# **post_measurements**
<a name="post_measurements"></a>
> post_measurements(measurement)



Create multiple measurements.

### Example

```python
import measurements_api_client
from measurements_api_client.apis.tags import measurements_api
from measurements_api_client.model.problem_details import ProblemDetails
from measurements_api_client.model.measurement import Measurement
from pprint import pprint
# Defining the host is optional and defaults to http://localhost:7001
# See configuration.py for a list of all supported configuration parameters.
configuration = measurements_api_client.Configuration(
    host = "http://localhost:7001"
)

# Enter a context with an instance of the API client
with measurements_api_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = measurements_api.MeasurementsApi(api_client)

    # example passing only required values which don't have defaults set
    body = [
        Measurement(
            id="id_example",
            time="1970-01-01T00:00:00.00Z",
            source="source_example",
            temperature=3.14,
            pressure=3.14,
            humidity=3.14,
            battery=3.14,
            acceleration=Acceleration(
                acceleration=3.14,
                acceleration_x=3.14,
                acceleration_y=3.14,
                acceleration_z=3.14,
            ),
        )
    ]
    try:
        api_response = api_instance.post_measurements(
            body=body,
        )
    except measurements_api_client.ApiException as e:
        print("Exception when calling MeasurementsApi->post_measurements: %s\n" % e)
```
### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
body | typing.Union[SchemaForRequestBodyApplicationJson] | required |
content_type | str | optional, default is 'application/json' | Selects the schema and serialization of the request body
accept_content_types | typing.Tuple[str] | default is ('application/json; charset&#x3D;utf-8', ) | Tells the server the content type(s) that are accepted by the client
stream | bool | default is False | if True then the response.content will be streamed and loaded from a file like object. When downloading a file, set this to True to force the code to deserialize the content to a FileSchema file
timeout | typing.Optional[typing.Union[int, typing.Tuple]] | default is None | the timeout used by the rest client
skip_deserialization | bool | default is False | when True, headers and body will be unset and an instance of api_client.ApiResponseWithoutDeserialization will be returned

### body

# SchemaForRequestBodyApplicationJson

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
list, tuple,  | tuple,  |  | 

### Tuple Items
Class Name | Input Type | Accessed Type | Description | Notes
------------- | ------------- | ------------- | ------------- | -------------
[**Measurement**]({{complexTypePrefix}}Measurement.md) | [**Measurement**]({{complexTypePrefix}}Measurement.md) | [**Measurement**]({{complexTypePrefix}}Measurement.md) |  | 

### Return Types, Responses

Code | Class | Description
------------- | ------------- | -------------
n/a | api_client.ApiResponseWithoutDeserialization | When skip_deserialization is True this response is returned
204 | [ApiResponseFor204](#post_measurements.ApiResponseFor204) | OK
default | [ApiResponseForDefault](#post_measurements.ApiResponseForDefault) | Unexpected error

#### post_measurements.ApiResponseFor204
Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
response | urllib3.HTTPResponse | Raw response |
body | Unset | body was not defined |
headers | Unset | headers were not defined |

#### post_measurements.ApiResponseForDefault
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

