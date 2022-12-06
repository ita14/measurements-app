# measurements_api_client.model.measurement.Measurement

Measurement data from one source

## Model Type Info
Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | -------------
dict, frozendict.frozendict,  | frozendict.frozendict,  | Measurement data from one source | 

### Dictionary Keys
Key | Input Type | Accessed Type | Description | Notes
------------ | ------------- | ------------- | ------------- | -------------
**source** | str,  | str,  | Source of measurement. With ruuvi tag this is MAC. | 
**time** | str, datetime,  | str,  | Measurement time as defined by RFC 3339, section 5.6, for example, 2017-07-21T17:32:28Z | value must conform to RFC-3339 date-time
**id** | str,  | str,  | Measurement unique identifier. Generated on insert. | [optional] 
**temperature** | decimal.Decimal, int, float,  | decimal.Decimal,  | Temperature in celsius. | [optional] value must be a 64 bit float
**pressure** | decimal.Decimal, int, float,  | decimal.Decimal,  | Pressure | [optional] value must be a 64 bit float
**humidity** | decimal.Decimal, int, float,  | decimal.Decimal,  | Humidity | [optional] value must be a 64 bit float
**battery** | decimal.Decimal, int, float,  | decimal.Decimal,  | Battery level. TBD what is the format. | [optional] value must be a 64 bit float
**acceleration** | [**Acceleration**](Acceleration.md) | [**Acceleration**](Acceleration.md) |  | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

