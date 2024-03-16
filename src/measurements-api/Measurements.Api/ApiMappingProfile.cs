using AutoMapper;
using Measurements.Api.Application.Commands.Sensors;

namespace Measurements.Api;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<OpenApi.Measurements.Api.Sensor, CreateSensorCommand>().ReverseMap();
        CreateMap<OpenApi.Measurements.Api.Sensor, UpdateSensorCommand>().ReverseMap();
        CreateMap<OpenApi.Measurements.Api.Sensor, Domain.Entities.Sensor>().ReverseMap();
        CreateMap<Domain.Entities.Sensor, CreateSensorCommand>().ReverseMap();
        CreateMap<Domain.Entities.Sensor, UpdateSensorCommand>().ReverseMap();

        CreateMap<OpenApi.Measurements.Api.Measurement, Domain.Entities.Measurement>().ReverseMap();
        CreateMap<OpenApi.Measurements.Api.Acceleration, Domain.Entities.Acceleration>().ReverseMap();
        CreateMap<Measurements.Api.Application.Queries.Measurements.MeasurementsDataResponse,
            OpenApi.Measurements.Api.MeasurementsDataResponse>();
    }
}
