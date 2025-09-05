using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace ApplicationCommon
{
    public static class ImageHelper
    {
        private static readonly string RootFolder = Path.Combine(Directory.GetCurrentDirectory(), AppConstants.ImageFolder);

        /// <summary>
        /// Saves uploaded image into UploadedImages/{tableName} folder
        /// and returns the saved image file name.
        /// </summary>

        public static async Task<string> SaveImageAsync(IFormFile file, string tableName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is null or empty");

            if (!Directory.Exists(RootFolder))
                Directory.CreateDirectory(RootFolder);

            var tableFolder = Path.Combine(RootFolder, tableName);
            if (!Directory.Exists(tableFolder))
                Directory.CreateDirectory(tableFolder);

            var originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);
            var sanitizedFileName = Regex.Replace(originalFileName, @"[^a-zA-Z0-9]", "_");
            var finalFileName = $"{sanitizedFileName}{extension}";
            var filePath = Path.Combine(tableFolder, finalFileName);
            int count = 1;
            while (File.Exists(filePath))
            {
                finalFileName = $"{sanitizedFileName}_{count}{extension}";
                filePath = Path.Combine(tableFolder, finalFileName);
                count++;
            }
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return finalFileName;
        }

        public static async Task<string> SaveImageWithAutoNameAsync(IFormFile file, string tableName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is null or empty");

            if (!Directory.Exists(RootFolder))
                Directory.CreateDirectory(RootFolder);

            var tableFolder = Path.Combine(RootFolder, tableName);
            if (!Directory.Exists(tableFolder))
                Directory.CreateDirectory(tableFolder);

            var fileExtension = Path.GetExtension(file.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(tableFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uniqueFileName;
        }
    }
}
