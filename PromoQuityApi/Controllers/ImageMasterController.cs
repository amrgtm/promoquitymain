using Application.DTOs.ApplicationImageMasterDTO;
using ApplicationCommon;
using ApplicationCommon.CustomException;
using ApplicationService.Interface;
using Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace PromoQuityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageMasterController : ControllerBase
    {
        private readonly ILogger<ImageMasterController> _logger;
        private readonly IImageMasterService _imageMasterService;
        private readonly CommonMessages _messages;
        public ImageMasterController(ILogger<ImageMasterController> logger, IImageMasterService imageMasterService, CommonMessages messages)
        {
            _logger = logger;
            _imageMasterService = imageMasterService;
            _messages = messages;
        }

        [HttpPost("CreateImageInfo")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateImageInfo([FromForm] CreateImageMasterDTO createImageDTO)
        {
            try
            {
                var result = await _imageMasterService.CreateImageInfoAsync(createImageDTO);
                return Ok(ApiResponse<object>.Ok(result, _messages.GetFriendlyMessage(CommonMessages.Mess.CreateSucceeded)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save image");
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }

        [HttpPut("UpdateImageInfo")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateImageInfo([FromForm] UpdateImageMasterDTO updateExamDTO)
        {
            try
            {
                var updatedExam = await _imageMasterService.UpdateImageInfoAsync(updateExamDTO);
                return Ok(ApiResponse<object>.Ok(updatedExam));
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

        [HttpDelete("DeleteImageInfo/{id}")]
        public async Task<IActionResult> DeleteImageInfo(long id)
        {
            try
            {
                var deletedImage = await _imageMasterService.DeleteImageInfoAsync(id);
                return Ok(ApiResponse<object>.Ok(deletedImage));
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
        [HttpGet("GetImageInfoById/{id}")]
        public async Task<IActionResult> GetImageInfoById(long id)
        {
            try
            {
                var imageInfo = await _imageMasterService.GetImageInfoByIdAsync(id);
                return Ok(ApiResponse<object>.Ok(imageInfo));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status404NotFound));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving image info");
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
        [HttpGet("GetPagedImageList")]
        public async Task<IActionResult> GetPagedImageList(int pageIndex = 1, int pageSize = 10, string table = null, string tableId = null, string imageName = null)
        {
            try
            {
                var pagedImages = await _imageMasterService.GetPagedImageListAsync(pageIndex, pageSize, table, tableId, imageName);
                return Ok(ApiResponse<object>.Ok(pagedImages));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving paged image list");
                return BadRequest(ApiResponse<object>.Fail(ex.Message, StatusCodes.Status400BadRequest));
            }
        }
    }
}
