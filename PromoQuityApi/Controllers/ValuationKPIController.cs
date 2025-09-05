using Application.DTOs.ApplicationValuationKPIDTO;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuationKPIController : ControllerBase
    {
        private readonly ILogger<ValuationKPIController> _logger;
        private readonly IValuationKPIService _valuationKPIService;
        private readonly CommonMessages _messages;
        public ValuationKPIController(ILogger<ValuationKPIController> logger, IValuationKPIService valuationKPIService, CommonMessages messages)
        {
            _logger = logger;
            _valuationKPIService = valuationKPIService;
            _messages = messages;
        }

        [HttpPost("CreateValuationKPI")]
        public async Task<IActionResult> CreateValuationKPI([FromBody] CreateValuationKPIDTO createValuationKPIDTO)
        {
            try
            {
                var result = await _valuationKPIService.CreateValuationKPIAsync(createValuationKPIDTO);
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
        [HttpPut("UpdateValuationKPI")]
        public async Task<IActionResult> UpdateValuationKPI([FromBody] UpdateValuationKPIDTO updateValuationKPIDTO)
        {
            try
            {
                var updatedKPI = await _valuationKPIService.UpdateValuationKPIAsync(updateValuationKPIDTO);
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
        [HttpDelete("DeleteValuationKPI/{id}")]
        public async Task<IActionResult> DeleteValuationKPI(long id)
        {
            try
            {
                var deletedKPI = await _valuationKPIService.DeleteValuationKPIAsync(id);
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
        [HttpGet("GetValuationKPIById/{id}")]
        public async Task<IActionResult> GetValuationKPIById(long id)
        {
            try
            {
                var valuationKPI = await _valuationKPIService.GetValuationKPIByIdAsync(id);
                if (valuationKPI == null)
                {
                    var message = _messages.GetFriendlyMessage(CommonMessages.Mess.NotFound);
                    return NotFound(ApiResponse<object>.Fail(message, StatusCodes.Status404NotFound));
                }
                return Ok(ApiResponse<object>.Ok(valuationKPI));
            }
            catch (Exception ex)
            {
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.RetrievalFailed);
                _logger.LogError(ex, message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
        [HttpGet("GetPagedValuationKPIList")]
        public async Task<IActionResult> GetPagedValuationKPIList([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, [FromQuery] string companyId = null)
        {
            try
            {
                var pagedList = await _valuationKPIService.GetPagedValuationKPIListAsync(pageIndex, pageSize, companyId);
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
