﻿using AutoMapper;
using HT366.Application.Dtos.Category;
using HT366.Application.Dtos.Exam;
using HT366.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT366.Application.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateExamDto, Exam>();
            CreateMap<Exam, ExamReadDto>();
            CreateMap<CategoryCreateDto, Category>();
        }
    }
}