using Application.DTOs.ApplicationFaqDTO;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : ControllerBase
    {
        private readonly ILogger<FaqController> _logger;
        private readonly IFaqService _faqService;
        private readonly CommonMessages _messages;
        public FaqController(ILogger<FaqController> logger, IFaqService faqService, CommonMessages messages)
        {
            _logger = logger;
            _faqService = faqService;
            _messages = messages;
        }

        [HttpPost("CreateFaq")]
        public async Task<IActionResult> CreateFaq([FromBody] CreateFaqDTO createFaqDTO)
        {
            try
            {
                var result = await _faqService.CreateFaqAsync(createFaqDTO);
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

        [HttpPut("UpdateFaq")]
        public async Task<IActionResult> UpdateFaq([FromBody] UpdateFaqDTO updateFaqDTO)
        {
            try
            {
                var updatedKPI = await _faqService.UpdateFaqAsync(updateFaqDTO);
                return Ok(ApiResponse<object>.Ok(updatedKPI));
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

        [HttpDelete("DeleteFaq/{id}")]
        public async Task<IActionResult> DeleteFaq(long id)
        {
            try
            {
                var deletedKPI = await _faqService.DeleteFaqAsync(id);
                return Ok(ApiResponse<object>.Ok(deletedKPI));
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

        [HttpGet("GetFaqById/{id}")]
        public async Task<IActionResult> GetFaqById(long id)
        {
            try
            {
                var kpi = await _faqService.GetFaqByIdAsync(id);
                return Ok(ApiResponse<object>.Ok(kpi));
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

        [HttpGet("GetPagedFaqList")]
        public async Task<IActionResult> GetPagedFaqList([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var pagedList = await _faqService.GetPagedFaqListAsync(pageIndex, pageSize);
                return Ok(ApiResponse<object>.Ok(pagedList));
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
