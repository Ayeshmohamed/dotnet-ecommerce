using Apps.Data;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Apps.Repository
{
    public class FilesUpload
    {

        private readonly IWebHostEnvironment _environment;

        public FilesUpload( IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public string UploadImages(string fileName, string folderName, IFormFile file)
        {
            // Added parentheses () to invoke the method
            var fName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";

            var uploadPath = Path.Combine(
                   _environment.WebRootPath,
                   "uploads",
                   folderName
                );

            Directory.CreateDirectory(uploadPath);
            var filePath = Path.Combine(uploadPath, fName);

            using var stream = new FileStream(
                filePath,
                FileMode.Create
            );

            // Also ensure you await CopyToAsync if your method is async, 
            // or use CopyTo() for synchronous execution.
            file.CopyTo(stream);

            var FilePath = $"/uploads/{folderName}/{fName}";

            return FilePath;
        }
    }
}
