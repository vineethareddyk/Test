using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using System.IO;
using System.Configuration;


namespace Blob_WebApp
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string storageAccountname = "julystorage";
            string accesskey = "pastekeyfromazureportal";
            string localFolder = ConfigurationManager.AppSettings["sourceFolder"];
            //create client object
            StorageCredentials storageCredentials = new StorageCredentials(storageAccountname, accesskey);
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            //create client to access storage
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("prodimg");
            //code to upload data to client             
            string[] fileEntries = Directory.GetFiles(localFolder);
            foreach(string filePath in fileEntries)
            {
                string key = Path.GetFileName(filePath);
                CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference(key);
                using(var fs=System.IO.File.Open(filePath,FileMode.Open,FileAccess.Read,FileShare.None))
                {
                    blob.UploadFromStream(fs);
                }
            }

            Label1.Text = "successfully uploaderd";
        }
    }
}