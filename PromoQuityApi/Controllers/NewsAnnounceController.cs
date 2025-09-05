using Application.DTOs.ApplicationNewsAnnounceDTO;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsAnnounceController : ControllerBase
    {
        private readonly ILogger<NewsAnnounceController> _logger;
        private readonly INewsAnnounceService _newsAnnounceService;
        private readonly CommonMessages _messages;
        public NewsAnnounceController(ILogger<NewsAnnounceController> logger, INewsAnnounceService newsAnnounceService, CommonMessages messages)
        {
            _logger = logger;
            _newsAnnounceService = newsAnnounceService;
            _messages = messages;
        }

        [HttpPost("CreateNews")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateNews([FromForm] CreateNewsAnnounceDTO createNewsAnnounceDTO)
        {
            try
            {
                var result = await _newsAnnounceService.CreateNewsAsync(createNewsAnnounceDTO);
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
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.CreateFailed);
                _logger.LogError(ex, message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }

        [HttpPut("UpdateNews")]
        public async Task<IActionResult> UpdateNews([FromForm] UpdateNewsAnnounceDTO updateNewsAnnounceDTO)
        {
            try
            {
                var updatedNews = await _newsAnnounceService.UpdateNewsAsync(updateNewsAnnounceDTO);
                return Ok(ApiResponse<object>.Ok(updatedNews));
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
        [HttpDelete("DeleteNews/{id}")]
        public async Task<IActionResult> DeleteNews(long id)
        {
            try
            {
                var deletedNews = await _newsAnnounceService.DeleteNewsAsync(id);
                return Ok(ApiResponse<object>.Ok(deletedNews));
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
        [HttpGet("GetNewsById/{id}")]
        public async Task<IActionResult> GetNewsById(long id)
        {
            try
            {
                var news = await _newsAnnounceService.GetNewsByIdAsync(id);
                return Ok(ApiResponse<object>.Ok(news));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (Exception ex)
            {
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.RetrievalFailed);
                _logger.LogError(ex, message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
        [HttpGet("GetPagedNewsList")]
        public async Task<IActionResult> GetPagedNewsList(int pageIndex = 1, int pageSize = 10, string title = null)
        {
            try
            {
                var pagedNews = await _newsAnnounceService.GetPagedNewsListAsync(pageIndex, pageSize, title);
                return Ok(ApiResponse<object>.Ok(pagedNews));
            }
            catch (Exception ex)
            {
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.RetrievalFailed);
                _logger.LogError(ex, message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
    }
}
