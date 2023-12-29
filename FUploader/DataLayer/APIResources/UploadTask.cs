
using FUploader.DataLayer.Validators;
using System.ComponentModel.DataAnnotations;

namespace FUploader.DataLayer.APIResources
{
    public class UploadTask
    {
        [Required]
        [FilePathValidator]
        public string? FilePath { get; set;}

        [Required]
        public string? Token { get; set; }

    }
}
