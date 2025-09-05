using Application.DTOs.ApplicationBlogDTO;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;
        private readonly IBlogService _blogService;
        private readonly CommonMessages _messages;
        public BlogController(ILogger<BlogController> logger, IBlogService blogService, CommonMessages messages)
        {
            _logger = logger;
            _blogService = blogService;
            _messages = messages;
        }

        [HttpPost("CreateBlog")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateBlog([FromForm] CreateBlogDTO createBlogDTO)
        {
            try
            {
                var result = await _blogService.CreateBlogAsync(createBlogDTO);
                return Ok(ApiResponse<object>.Ok(result));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (DuplicateEntryException)
            {
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.DuplicateEntry);
                return BadRequest(ApiResponse<object>.Fail(message, StatusCodes.Status400BadRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating role");
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }

        [HttpPut("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog([FromBody] UpdateBlogDTO updateBlogDTO)
        {
            try
            {
                var updatedExam = await _blogService.UpdateBlogAsync(updateBlogDTO);
                return Ok(ApiResponse<object>.Ok(updatedExam));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (DuplicateEntryException)
            {
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.DuplicateEntry);
                return BadRequest(ApiResponse<object>.Fail(message, StatusCodes.Status400BadRequest));
            }
            catch (Exception ex)
            {
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.UpdateFailed);
                _logger.LogError(ex, message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }

        [HttpDelete("DeleteBlog/{id}")]
        public async Task<IActionResult> DeleteBlog(long id)
        {
            try
            {
                var deletedExam = await _blogService.DeleteBlogAsync(id);
                return Ok(ApiResponse<object>.Ok(deletedExam));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (Exception ex)
            {
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.DeleteFailed);
                _logger.LogError(ex, message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }


        [HttpGet("GetBlogById/{id}")]
        public async Task<IActionResult> GetBlogById(long id)
        {
            try
            {
                var exam = await _blogService.GetBlogByIdAsync(id);
                return Ok(ApiResponse<object>.Ok(exam));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (Exception ex)
            {
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.BadRequest);
                _logger.LogError(ex, message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }

        [HttpGet("GetPagedBlogList")]
        public async Task<IActionResult> GetPagedBlogList(int pageIndex = 1, int pageSize = 10, string blogId = null, string blogTitle = null)
        {
            try
            {
                var exams = await _blogService.GetPagedBlogListAsync(pageIndex, pageSize,blogId, blogTitle);
                return Ok(ApiResponse<object>.Ok(exams));
            }
            catch (Exception ex)
            {
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.BadRequest);
                _logger.LogError(ex, message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
    }
}
