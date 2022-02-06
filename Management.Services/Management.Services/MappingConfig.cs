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
             config.CreateMap<DownloadDto, Download>();
             config.CreateMap<Download, DownloadDto>();
             config.CreateMap<StudentDto, Student>();
             config.CreateMap<Student, StudentDto>();
             config.CreateMap<Exam, ExamDto>();
             config.CreateMap<ExamDto, Exam>();
             config.CreateMap<ExamCreateDto, Exam>();
             config.CreateMap<Exam, ExamCreateDto>();
             config.CreateMap<StudentExamDto, StudentExam>();
             config.CreateMap<StudentExam, StudentExamDto>();
        });

        return mappingConfig;
    }
}