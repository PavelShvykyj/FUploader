
using Firebase.Storage;
using System.IO;

namespace FUploader.Core.FireBase
{
    public class UploadManager
    {
        private string _uploadFolder;
        private string _bucket;
        public delegate void FileUploadedEventHendler(bool resoltness, string resolt);
        public event FileUploadedEventHendler OnFileUploaded;


        public UploadManager(IConfiguration configuration)
        {
            _uploadFolder = configuration.GetSection("Firebase").GetValue<string>("uploadFolder") ?? "";
            _bucket = configuration.GetSection("Firebase").GetValue<string>("Bucket") ?? "";
        }

        public async Task<string> UploadFile(string localPath, string token) {

            using (var stream = File.Open(localPath, FileMode.Open))
            {

                var task = new FirebaseStorage(
                    _bucket,
                     new FirebaseStorageOptions
                     {
                         AuthTokenAsyncFactory = () => Task.FromResult(token),
                         ThrowOnCancel = true,
                     })
                    .Child(_uploadFolder)
                    .Child(Path.GetFileName(localPath))
                    .PutAsync(stream);


                // await the task to wait until upload completes and get the download url
                var downloadUrl = await task;
                if (OnFileUploaded != null) {
                    OnFileUploaded(true, downloadUrl);
                }                
                return downloadUrl;
            }
        }    
    }
}
