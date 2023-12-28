
using FUploader.DataLayer.Validators;
using System.ComponentModel.DataAnnotations;

namespace FUploader.DataLayer.APIResources
{
    public class UploadTask
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        [FilePathValidator]
        public string? FilePath { get; set;}

    }
}
