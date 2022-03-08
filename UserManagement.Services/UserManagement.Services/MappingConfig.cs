using AutoMapper;
using UserManagement.Services.Dtos;
using UserManagement.Services.Models;

namespace UserManagement.Services;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ApplicationUser, UserDto>();
        });

        return mappingConfig;
    }
}