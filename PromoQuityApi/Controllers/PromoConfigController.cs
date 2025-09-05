using Application.DTOs.ApplicationPromoConfigDTO;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoConfigController : ControllerBase
    {
        private readonly ILogger<PromoConfigController> _logger;
        private readonly IPromoConfigService _promoConfigService;
        private readonly CommonMessages _messages;
        public PromoConfigController(ILogger<PromoConfigController> logger, IPromoConfigService promoConfigService, CommonMessages messages)
        {
            _logger = logger;
            _promoConfigService = promoConfigService;
            _messages = messages;
        }

        [HttpPost("CreatePromoConfig")]
        public async Task<IActionResult> CreatePromoConfig([FromBody] CreatePromoConfigDTO createPromoConfigDTO)
        {
            try
            {
                var result = await _promoConfigService.CreatePromoConfigAsync(createPromoConfigDTO);
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

        [HttpPut("UpdatePromoConfig")]
        public async Task<IActionResult> UpdatePromoConfig([FromBody] UpdatePromoConfigDTO updatePromoConfigDTO)
        {
            try
            {
                var updatedKPI = await _promoConfigService.UpdatePromoConfigAsync(updatePromoConfigDTO);
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

        [HttpDelete("DeletePromoConfig/{id}")]
        public async Task<IActionResult> DeletePromoConfig(long id)
        {
            try
            {
                var deletedKPI = await _promoConfigService.DeletePromoConfigAsync(id);
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

        [HttpGet("GetPromoConfigById/{id}")]
        public async Task<IActionResult> GetPromoConfigById(long id)
        {
            try
            {
                var kpi = await _promoConfigService.GetPromoConfigByIdAsync(id);
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

        [HttpGet("GetPagedPromoConfigList")]
        public async Task<IActionResult> GetPagedPromoConfigList([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var pagedList = await _promoConfigService.GetPagedPromoConfigListAsync(pageIndex, pageSize);
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
