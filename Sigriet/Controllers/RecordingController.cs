using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using System;
using System.IO;
using System.Web;
using System.Web.Http;

namespace Sigriet.Controllers
{
    public class RecordingController : ApiController
    {
        public void Add()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ?
                HttpContext.Current.Request.Files[0] : null;

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                var storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));

                var blobClient = storageAccount.CreateCloudBlobClient();

                var container = blobClient.GetContainerReference("calls");

                var blockBlob = container.GetBlockBlobReference($"{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")}.wav");

                blockBlob.UploadFromStream(file.InputStream);
            }
        }
    }
}
