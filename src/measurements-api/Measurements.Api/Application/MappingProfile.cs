using AutoMapper;
using Measurements.Api.Application.Measurements.Queries;
using Measurements.Api.Application.Sensors.Commands;

namespace Measurements.Api.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OpenApi.Measurements.Api.Sensor, CreateSensorCommand>().ReverseMap();
        CreateMap<OpenApi.Measurements.Api.Sensor, UpdateSensorCommand>().ReverseMap();
        CreateMap<OpenApi.Measurements.Api.Sensor, Domain.Entities.Sensor>().ReverseMap();
        CreateMap<Domain.Entities.Sensor, CreateSensorCommand>().ReverseMap();
        CreateMap<Domain.Entities.Sensor, UpdateSensorCommand>().ReverseMap();

        CreateMap<OpenApi.Measurements.Api.Measurement, Domain.Entities.Measurement>().ReverseMap();
        CreateMap<OpenApi.Measurements.Api.Acceleration, Domain.Entities.Acceleration>().ReverseMap();
        CreateMap<OpenApi.Measurements.Api.MeasurementFilter, SearchMeasurementsQuery>();
    }
}
