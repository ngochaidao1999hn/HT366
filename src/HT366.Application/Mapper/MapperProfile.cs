using AutoMapper;
using HT366.Application.Dtos.Category;
using HT366.Application.Dtos.Exam;
using HT366.Application.Dtos.File;
using HT366.Domain.Entities;

namespace HT366.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateExamDto, Exam>();
            CreateMap<Exam, ExamReadDto>();
            CreateMap<Domain.Entities.File, FileReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category, CategoryReadDto>();
        }
    }
}