using AutoMapper;
using Management.Services.Dtos;
using Management.Services.Models;

namespace Management.Services;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
             config.CreateMap<Student, CheckQrDto>();
        });

        return mappingConfig;
    }
}