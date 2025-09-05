using Application.DTOs.ApplicationMarketKPIDTO;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketKPIController : ControllerBase
    {
        private readonly ILogger<MarketKPIController> _logger;
        private readonly IMarketKPIService _marketKPIService;
        private readonly CommonMessages _messages;
        public MarketKPIController(ILogger<MarketKPIController> logger, IMarketKPIService marketKPIService, CommonMessages messages)
        {
            _logger = logger;
            _marketKPIService = marketKPIService;
            _messages = messages;
        }

        [HttpPost("CreateMarketKPI")]
        public async Task<IActionResult> CreateMarketKPI([FromBody] CreateMarketKPIDTO createMarketKPIDTO)
        {
            try
            {
                var result = await _marketKPIService.CreateMarketKPIAsync(createMarketKPIDTO);
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

        [HttpPut("UpdateMarketKPI")]
        public async Task<IActionResult> UpdateMarketKPI([FromBody] UpdateMarketKPIDTO updateMarketKPIDTO)
        {
            try
            {
                var updatedKPI = await _marketKPIService.UpdateMarketKPIAsync(updateMarketKPIDTO);
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

        [HttpDelete("DeleteMarketKPI/{id}")]
        public async Task<IActionResult> DeleteMarketKPI(long id)
        {
            try
            {
                var deletedKPI = await _marketKPIService.DeleteMarketKPIAsync(id);
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

        [HttpGet("GetMarketKPIById/{id}")]
        public async Task<IActionResult> GetMarketKPIById(long id)
        {
            try
            {
                var kpi = await _marketKPIService.GetMarketKPIByIdAsync(id);
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

        [HttpGet("GetPagedMarketKPIList")]
        public async Task<IActionResult> GetPagedMarketKPIList([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, [FromQuery] string companyId = null)
        {
            try
            {
                var pagedList = await _marketKPIService.GetPagedMarketKPIListAsync(pageIndex, pageSize, companyId);
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
