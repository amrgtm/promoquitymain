using Application.DTOs.ApplicationDownloadDTO;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadFilesController : ControllerBase
    {
        private readonly ILogger<DownloadFilesController> _logger;
        private readonly IDownloadService _downloadService;
        private readonly CommonMessages _messages;
        public DownloadFilesController(ILogger<DownloadFilesController> logger, IDownloadService downloadService, CommonMessages messages)
        {
            _logger = logger;
            _downloadService = downloadService;
            _messages = messages;
        }

        [HttpPost("CreateDownloadInfo")]
        public async Task<IActionResult> CreateDownloadInfo([FromBody] CreateDownloadDTO createDownloadDTO)
        {
            try
            {
                var result = await _downloadService.CreateDownloadInfoAsync(createDownloadDTO);
                return Ok(ApiResponse<object>.Ok(result));
            }
            catch (DuplicateEntryException)
            {
                var message = _messages.GetFriendlyMessage(CommonMessages.Mess.DuplicateEntry);
                return BadRequest(ApiResponse<object>.Fail(message, StatusCodes.Status400BadRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating role");
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }

        [HttpPut("UpdateDownloadInfo")]
        public async Task<IActionResult> UpdateDownloadInfo([FromBody] UpdateDownloadDTO updateDownloadDTO)
        {
            try
            {
                var updatedDownload = await _downloadService.UpdateDownloadInfoAsync(updateDownloadDTO);
                return Ok(ApiResponse<object>.Ok(updatedDownload));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
        [HttpDelete("DeleteDownloadInfo/{id}")]
        public async Task<IActionResult> DeleteDownloadInfo(long id)
        {
            try
            {
                var result = await _downloadService.DeleteDownloadInfoAsync(id);
                return Ok(ApiResponse<object>.Ok(result));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
        [HttpGet("GetDownloadDataById/{id}")]
        public async Task<IActionResult> GetDownloadDataById(long id)
        {
            try
            {
                var result = await _downloadService.GetDownloadDataByIdAsync(id);
                return Ok(ApiResponse<object>.Ok(result));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
        [HttpGet("GetPagedDownloadList")]
        public async Task<IActionResult> GetPagedDownloadList(int pageIndex, int pageSize, string docTitle = null, string docName = null)
        {
            try
            {
                var result = await _downloadService.GetPagedDownloadListAsync(pageIndex, pageSize, docTitle, docName);
                return Ok(ApiResponse<object>.Ok(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
    }
}
