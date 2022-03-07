using AutoMapper;

namespace UserManagement.Services;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            //config.CreateMap<Student, CheckQrDto>();
            
        });

        return mappingConfig;
    }
}