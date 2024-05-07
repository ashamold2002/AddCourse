using AutoMapper;
using LXP.Common.Entities;
using LXP.Common.ViewModels;
using LXP.Data.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using LXP.Core.IServices;
using Org.BouncyCastle.Asn1.Ocsp;

namespace LXP.Core.Services
{
    public class CourseServices : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILevelRepository _levelRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;

        public CourseServices(ICourseRepository courseRepository, IWebHostEnvironment environment, ICategoryRepository categoryRepository, ILevelRepository levelRepository,IHttpContextAccessor contextAccessor)
        {
            this._courseRepository = courseRepository;
            this._categoryRepository = categoryRepository;
            _courseRepository = courseRepository;
            _environment = environment;

            var configAddCourse = new MapperConfiguration(cfg => cfg.CreateMap<Course, CourseModel>().ReverseMap());
            _mapper = new Mapper(configAddCourse);
            _categoryRepository = categoryRepository;
            _levelRepository = levelRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task AddCourse(CourseModel course)
        {
            // Course courses = _mapper.Map<Course,CourseModel>(course);

            // Generate a unique file name
            var uniqueFileName = $"{Guid.NewGuid()}_{course.Thumbnailimage.FileName}";

            // Save the image to a designated folder (e.g., wwwroot/images)
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "CourseThumbnailImages"); // Use WebRootPath
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await course.Thumbnailimage.CopyToAsync(stream); // Use await
            }

            // Store the file path in the database

            
            var level = await _levelRepository.GetLevel(course.Level);

            var category = await _categoryRepository.GetCategory(course.Catagory);
            Course courses = new Course
            {
                CourseId = Guid.NewGuid(),
                CatagoryId = category.CatagoryId,
                LevelId= level.LevelId,
                Title = course.Title,
                Description = course.Description,
                Duration = course.Duration,
                Thumbnail = uniqueFileName,
                CreatedBy = "Admin",
                CreatedAt = new DateTime(),
                IsActive=true,
                IsAvailable=true,
                ModifiedAt = new DateTime(),
                ModifiedBy="Admin"

                
            };
            await _courseRepository.AddCourse(courses);
                

        }

        public async Task<List<CourseModel>> getCourse()
        {
               List<Course> courses =await _courseRepository.getCourse();
            List<CourseModel> courseModels = new List<CourseModel>();

            foreach (var item in courses)
            {
                var course = new CourseModel
                {
                    Title = item.Title,
                    Description = item.Description,
                    Catagory = item.Catagory.Category,
                    Level = item.Level.Level,
                    Duration = item.Duration,
                    //Thumbnailimage = String.Format("{0}://{1}{2}/wwwroot/CourseThumbnailImages/{3}",_contextAccessor.Request.Schema, _contextAccessor.Request.Host, _contextAccessor.Request.PathBase, item.Thumbnail)

                };

               courseModels.Add(course);
            }

            return courseModels;
        }
    }
}
