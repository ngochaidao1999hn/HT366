﻿using AutoFixture;
using AutoMapper;
using HT366.Application.Dtos.Exam;
using HT366.Application.Mapper;
using HT366.Application.Services;
using HT366.Domain.Common.Enums;
using HT366.Domain.Entities;
using HT366.Domain.Interfaces;
using HT366.Infrastructure.Services;
using HT366.Test.Utils;
using Moq;

namespace HT366.Test
{
    public class ExamTest
    {
        private readonly Fixture fixture = new();
        private readonly Mock<IUnitOfWork> unitOfWork = new();
        private readonly MockRepository<Exam> examRepository = new();
        private readonly MockRepository<Category> categoryRepository = new();
        private ExamService examService;
        private CategoryService categoryService;
        private FileService fileService;

        #region Init
        public ExamTest()
        {
            unitOfWork.Setup(uow => uow.examRepository).Returns(examRepository);
            unitOfWork.Setup(uow => uow.categoryRepository).Returns(categoryRepository);

            var myProfile = new MapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);
            categoryService = new CategoryService(unitOfWork.Object, mapper);
            fileService = new FileService();
            examService = new ExamService(unitOfWork.Object, mapper, categoryService, fileService);
        }
        #endregion

        #region CreateData
        private async Task<Category> CreateCategory()
        {
            var cate = fixture.Build<Category>()
                   .With(x => x.Id, Helper.GenerateId())
                   .With(x => x.Name, "Test Category")
                   .With(x => x.Description, "Test Category")
                   .With(x => x.IsDeleted, false)
                   .Without(x => x.Exams)
                   .Without(x => x.Exercises)
                   .Without(x => x.Lessons)
                   .Create();
            await categoryRepository.CreateAsync(cate);
            return cate;
        }

        private Exam CreateExam(Category cate)
        {
            return fixture.Build<Exam>()
                .With(x => x.Id, Helper.GenerateId())
                .With(x => x.Name, "Test Exam")
                .With(x => x.Content, "Test Content")
                .With(x => x.Description, "Test Description")
                .With(x => x.CategoryId, cate.Id)
                .With(x => x.Category, cate)
                .With(x => x.IsDeleted, false)
                .With(x => x.Status, StatusEnum.Pending)
                .Create();
        }
        #endregion

        #region Test
        [Fact]
        public async Task Add_Exam_Success()
        {
            //var cate = await CreateCategory();
            //var exam = CreateExam(cate);
            //var res =  await examService.Insert(exam);

            //Assert.Equal(exam.Id, res);
        }
        #endregion
    }
}