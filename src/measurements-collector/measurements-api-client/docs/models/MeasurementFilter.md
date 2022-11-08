# measurements_api_client.model.measurement_filter.MeasurementFilter

Filters for measurement items.

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
dict, frozendict.frozendict,  | frozendict.frozendict,  | Filters for measurement items. | 

### Dictionary Keys
Key | Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | ------------- | -------------
**startTime** | None, str, datetime,  | NoneClass, str,  | Start time as defined by RFC 3339, section 5.6. | [optional] value must conform to RFC-3339 date-time
**endTime** | None, str, datetime,  | NoneClass, str,  | End time as defined by RFC 3339, section 5.6. | [optional] value must conform to RFC-3339 date-time
**source** | None, str,  | NoneClass, str,  | Measurement source identifier. | [optional] 
**orderBy** | str,  | str,  | Order results by column. Format is column_name:sort_direction. | [optional] if omitted the server will use the default value of "time:asc"
**limit** | decimal.Decimal, int,  | decimal.Decimal,  | Maximum number of results to return | [optional] if omitted the server will use the default value of 100
**offset** | decimal.Decimal, int,  | decimal.Decimal,  | Starting offset | [optional] if omitted the server will use the default value of 0

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

