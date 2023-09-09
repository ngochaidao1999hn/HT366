using HT366.Application.Dtos.Exam;
using HT366.Application.Services;
using HT366.Infrastructure.Utils.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HT366.API.Controllers
{
    public class ExamController : BaseController
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> PostExam([FromBody] CreateExamDto dto)
        {
            try
            {
                var userId = GetUserId();
                var newExamId = await _examService.Insert(userId, dto);
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

        [HttpPost("{id}/verify")]
        public async Task<IActionResult> VerifyExam(Guid id, [FromBody] ExamApprovalDto dto)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamById(Guid id)
        {
            return Ok(await _examService.GetById(id));
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(Guid id)
        {
            try
            {
                var userId = GetUserId();
                var exam = await _examService.GetById(id);
                if (exam is null)
                {
                    return NotFound("Exam Not Found");
                }
                if (exam.CreatedBy != userId)
                {
                    return Forbid();
                }
                await _examService.Delete(exam.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}