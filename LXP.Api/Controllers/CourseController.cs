using LXP.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LXP.Core.IServices;
namespace LXP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddCourseController : BaseController
    {
        private readonly ICourseService _CourseService;
        public AddCourseController(ICourseService addCourseService)
        {
            _CourseService = addCourseService;

        }

        [HttpPost("/lxp/addcourse")]
        public async Task<ActionResult> AddCourse([FromForm] CourseModel courseModel)
        {
            var createCourse = _CourseService.AddCourse(courseModel);

            return Ok(CreateSuccessResponse(createCourse));
        }

        [HttpGet("/lxp/viewcourse")]
        public async Task<ActionResult<List<CourseModel>>> GetCourse()
        {
            var getCourse = _CourseService.getCourse();
            if(getCourse == null)
            {
                return NotFound();
            }
            return Ok(CreateSuccessResponse(getCourse));
        }


    }
}
