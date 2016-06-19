using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigriet
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference("calls");

            var blobs = container.ListBlobs(null, true).Reverse();

            return View(blobs);
        }
    }
}