using Application.DTOs.ApplicationHomeContentMidDTO;
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
    public class HomeContentMidController : ControllerBase
    {
        private readonly ILogger<HomeContentMidController> _logger;
        private readonly IHomeContentMidService _homeContentService;
        private readonly CommonMessages _messages;
        public HomeContentMidController(ILogger<HomeContentMidController> logger, IHomeContentMidService homeContentService, CommonMessages messages)
        {
            _logger = logger;
            _homeContentService = homeContentService;
            _messages = messages;
        }

        [HttpPost("CreateHomeContentMid")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateHomeContentMid([FromForm] CreateHomeContentMidDTO createHomeContentMidDTO)
        {
            try
            {
                var result = await _homeContentService.CreateHomeContentMidAsync(createHomeContentMidDTO);
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
        [HttpPut("UpdateHomeContentMid")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateHomeContentMid([FromForm] UpdateHomeContentMidDTO updateHomeContentMidDTO)
        {
            try
            {
                var updatedHomeContentMid = await _homeContentService.UpdateHomeContentMidAsync(updateHomeContentMidDTO);
                return Ok(ApiResponse<object>.Ok(updatedHomeContentMid));
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
        [HttpDelete("DeleteHomeContentMid/{id}")]
        public async Task<IActionResult> DeleteHomeContentMid(long id)
        {
            try
            {
                var deletedHomeContentMid = await _homeContentService.DeleteHomeContentMidAsync(id);
                return Ok(ApiResponse<object>.Ok(deletedHomeContentMid));
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
        [HttpGet("GetHomeContentMidById/{id}")]
        public async Task<IActionResult> GetHomeContentMidById(long id)
        {
            try
            {
                var homeContent = await _homeContentService.GetHomeContentMidByIdAsync(id);
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
        [HttpGet("GetPagedHomeContentMidList")]
        public async Task<IActionResult> GetPagedHomeContentMidList(int pageIndex = 1, int pageSize = 10, string topic = null)
        {
            try
            {
                var pagedHomeContentMidList = await _homeContentService.GetPagedHomeContentMidListAsync(pageIndex, pageSize, topic);
                return Ok(ApiResponse<object>.Ok(pagedHomeContentMidList));
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
