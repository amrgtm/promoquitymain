using Application.DTOs.ApplicationCompanyProfileDTO;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyProfileController : ControllerBase
    {
        private readonly ILogger<CompanyProfileController> _logger;
        private readonly ICompanyProfileService _companyProfileService;
        private readonly CommonMessages _messages;
        public CompanyProfileController(ILogger<CompanyProfileController> logger, ICompanyProfileService companyProfileService, CommonMessages messages)
        {
            _logger = logger;
            _companyProfileService = companyProfileService;
            _messages = messages;
        }

        [HttpPost("CreateCompanyProfile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateCompanyProfile([FromForm] CreateCompanyProfileDTO createCompanyDTO)
        {
            try
            {
                var result = await _companyProfileService.CreateCompanyProfileAsync(createCompanyDTO);
                return Ok(ApiResponse<object>.Ok(result));
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

        [HttpPut("UpdateCompanyProfile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateCompanyProfile([FromForm] UpdateCompanyProfileDTO updateCompanyDTO)
        {
            try
            {
                var updatedCompany = await _companyProfileService.UpdateCompanyProfileAsync(updateCompanyDTO);
                return Ok(ApiResponse<object>.Ok(updatedCompany));
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
        [HttpDelete("DeleteCompanyProfile/{id}")]
        public async Task<IActionResult> DeleteCompanyProfile(long id)
        {
            try
            {
                var deletedCompany = await _companyProfileService.DeleteCompanyProfileAsync(id);
                return Ok(ApiResponse<object>.Ok(deletedCompany));
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
        [HttpGet("GetCompanyProfileById/{id}")]
        public async Task<IActionResult> GetCompanyProfileById(long id)
        {
            try
            {
                var company = await _companyProfileService.GetCompanyProfileByIdAsync(id);
                return Ok(ApiResponse<object>.Ok(company));
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
        [HttpGet("GetPagedCompanyProfileList")]
        public async Task<IActionResult> GetPagedCompanyProfileList(int pageIndex=1, int pageSize=10, string? companyId = null, string? companyName = null)
        {
            try
            {
                var companies = await _companyProfileService.GetPagedCompanyProfileListAsync(pageIndex, pageSize, companyId, companyName);
                return Ok(ApiResponse<object>.Ok(companies));
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
