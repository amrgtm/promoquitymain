using Application.DTOs.ApplicationOperationalKPIDTO;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationalKPIController : ControllerBase
    {
        private readonly ILogger<OperationalKPIController> _logger;
        private readonly IOperationalKPIService _operationalKPIService;
        private readonly CommonMessages _messages;
        public OperationalKPIController(ILogger<OperationalKPIController> logger, IOperationalKPIService operationalKPIService, CommonMessages messages)
        {
            _logger = logger;
            _operationalKPIService = operationalKPIService;
            _messages = messages;
        }

        [HttpPost("CreateOperationalKPI")]
        public async Task<IActionResult> CreateOperationalKPI([FromBody] CreateOperationalKPIDTO createOperationalKPIDTO)
        {
            try
            {
                var result = await _operationalKPIService.CreateOperationKPIAsync(createOperationalKPIDTO);
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

        [HttpPut("UpdateOperationalKPI")]
        public async Task<IActionResult> UpdateOperationalKPI([FromBody] UpdateOperationalKPIDTO updateOperationalKPIDTO)
        {
            try
            {
                var updatedKPI = await _operationalKPIService.UpdateOperationKPIAsync(updateOperationalKPIDTO);
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

        [HttpDelete("DeleteOperationalKPI/{id}")]
        public async Task<IActionResult> DeleteOperationalKPI(long id)
        {
            try
            {
                var deletedKPI = await _operationalKPIService.DeleteOperationKPIAsync(id);
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

        [HttpGet("GetOperationalKPIById/{id}")]
        public async Task<IActionResult> GetOperationalKPIById(long id)
        {
            try
            {
                var kpi = await _operationalKPIService.GetOperationKPIByIdAsync(id);
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

        [HttpGet("GetPagedOperationalKPIList")]
        public async Task<IActionResult> GetPagedOperationalKPIList([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, [FromQuery] string companyId = null)
        {
            try
            {
                var pagedKPIs = await _operationalKPIService.GetPagedOperationKPIListAsync(pageIndex, pageSize, companyId);
                return Ok(ApiResponse<object>.Ok(pagedKPIs));
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
