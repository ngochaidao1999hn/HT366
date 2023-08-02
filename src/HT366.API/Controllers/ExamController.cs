using HT366.Application.Dtos.Exam;
using HT366.Application.Services;
using HT366.Infrastructure.Utils.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HT366.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;
        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostExam([FromBody] CreateExamDto dto)
        {
            try
            {
                var newExamId = await _examService.Insert(dto);
                return Ok(newExamId);
            }
            catch (ResourceNotFoundException rsnf)
            {
                return NotFound(rsnf.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }        
        }

        [HttpGet]
        public async Task<IActionResult> GetExams([FromQuery] GetExamFilter filter)
        {
            try
            {
                return Ok(await _examService.GetAll(filter));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
