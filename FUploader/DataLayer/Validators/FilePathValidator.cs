using System.ComponentModel.DataAnnotations;


namespace FUploader.DataLayer.Validators
{
    public class FilePathValidator: ValidationAttribute
    {

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            IConfiguration configuration = (IConfiguration)validationContext
                .GetService(typeof(IConfiguration));

            int maxLenth = configuration.GetSection("MaxFileLength").Get<int>();
           
            if (value is null) 
            {
                return new ValidationResult("Empty parh");
            } 

            string path = value.ToString() ?? "";

            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Exists) 
            {
                return new ValidationResult("File is not exists");
            }

            if (fileInfo.Length > maxLenth * 1024 * 1024) 
            {
                return new ValidationResult("File is too large");
            }
            
            return ValidationResult.Success;
        }
    }
}
