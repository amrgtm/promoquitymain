using Application.DTOs.ApplicationHomeContentDTO;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using ApplicationService.Implementation;
using ApplicationService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeContentController : ControllerBase
    {
        private readonly ILogger<HomeContentController> _logger;
        private readonly IHomeContentService _homeContentService;
        private readonly CommonMessages _messages;
        public HomeContentController(ILogger<HomeContentController> logger, IHomeContentService homeContentService, CommonMessages messages)
        {
            _logger = logger;
            _homeContentService = homeContentService;
            _messages = messages;
        }

        [HttpPost("CreateHomeContent")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateHomeContent([FromForm] CreateHomeContentDTO createHomeContentDTO)
        {
            try
            {
                var result = await _homeContentService.CreateHomeContentAsync(createHomeContentDTO);
                return Ok(ApiResponse<object>.Ok(result));
            }
            catch (NotFoundException)
            {
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.NotFound);
                return NotFound(ApiResponse<object>.Fail(message, StatusCodes.Status404NotFound));
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
        [HttpPut("UpdateHomeContent")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateHomeContent([FromForm] UpdateHomeContentDTO updateHomeContentDTO)
        {
            try
            {
                var updatedHomeContent = await _homeContentService.UpdateHomeContentAsync(updateHomeContentDTO);
                return Ok(ApiResponse<object>.Ok(updatedHomeContent));
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
        [HttpDelete("DeleteHomeContent/{id}")]
        public async Task<IActionResult> DeleteHomeContent(long id)
        {
            try
            {
                var deletedHomeContent = await _homeContentService.DeleteHomeContentAsync(id);
                return Ok(ApiResponse<object>.Ok(deletedHomeContent));
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
        [HttpGet("GetHomeContentById/{id}")]
        public async Task<IActionResult> GetHomeContentById(long id)
        {
            try
            {
                var homeContent = await _homeContentService.GetHomeContentByIdAsync(id);
                return Ok(ApiResponse<object>.Ok(homeContent));
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
        [HttpGet("GetPagedHomeContentList")]
        public async Task<IActionResult> GetPagedHomeContentList(int pageIndex = 1, int pageSize = 10, string topic = null)
        {
            try
            {
                var pagedHomeContentList = await _homeContentService.GetPagedHomeContentListAsync(pageIndex, pageSize, topic);
                return Ok(ApiResponse<object>.Ok(pagedHomeContentList));
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
