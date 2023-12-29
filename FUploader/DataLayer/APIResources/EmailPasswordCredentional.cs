using System.ComponentModel.DataAnnotations;

namespace FUploader.DataLayer.APIResources
{
    public class EmailPasswordCredentional
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
