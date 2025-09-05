using Application.DTOs.ApplicationFinancialKPIDTO;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialKPIController : ControllerBase
    {
        private readonly ILogger<FinancialKPIController> _logger;
        private readonly IFinancialKPIService _financialKPIService;
        private readonly CommonMessages _messages;
        public FinancialKPIController(ILogger<FinancialKPIController> logger, IFinancialKPIService financialKPIService, CommonMessages messages)
        {
            _logger = logger;
            _financialKPIService = financialKPIService;
            _messages = messages;
        }

        [HttpPost("CreateFinancialKPI")]
        public async Task<IActionResult> CreateFinancialKPI([FromBody] CreateFinancialKPIDTO createFinancialKPIDTO)
        {
            try
            {
                var result = await _financialKPIService.CreateFinancialKPIAsync(createFinancialKPIDTO);
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

        [HttpPut("UpdateFinancialKPI")]
        public async Task<IActionResult> UpdateFinancialKPI([FromBody] UpdateFinancialKPIDTO updateFinancialKPIDTO)
        {
            try
            {
                var updatedKPI = await _financialKPIService.UpdateFinancialKPIAsync(updateFinancialKPIDTO);
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

        [HttpDelete("DeleteFinancialKPI/{id}")]
        public async Task<IActionResult> DeleteFinancialKPI(long id)
        {
            try
            {
                var deletedKPI = await _financialKPIService.DeleteFinancialKPIAsync(id);
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

        [HttpGet("GetFinancialKPIById/{id}")]
        public async Task<IActionResult> GetFinancialKPIById(long id)
        {
            try
            {
                var kpi = await _financialKPIService.GetFinancialKPIByIdAsync(id);
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

        [HttpGet("GetPagedFinancialKPIList")]
        public async Task<IActionResult> GetPagedFinancialKPIList(int pageIndex = 1, int pageSize = 10, string companyId = null)
        {
            try
            {
                var pagedList = await _financialKPIService.GetPagedFinancialKPIListAsync(pageIndex, pageSize, companyId);
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
