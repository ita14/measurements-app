# measurements_api_client.model.measurements_data_response.MeasurementsDataResponse

Response object for measurements.

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
dict, frozendict.frozendict,  | frozendict.frozendict,  | Response object for measurements. | 

### Dictionary Keys
Key | Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | ------------- | -------------
**count** | decimal.Decimal, int,  | decimal.Decimal,  | Count of items returned. | [optional] 
**total** | decimal.Decimal, int,  | decimal.Decimal,  | Total number of items. | [optional] 
**[items](#items)** | list, tuple,  | tuple,  |  | [optional] 

# items

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
list, tuple,  | tuple,  |  | 

### Tuple Items
Class Name | Input Type | Accessed Type | Description | Notes
------------- | ------------- | ------------- | ------------- | -------------
[**Measurement**](Measurement.md) | [**Measurement**](Measurement.md) | [**Measurement**](Measurement.md) |  | 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

