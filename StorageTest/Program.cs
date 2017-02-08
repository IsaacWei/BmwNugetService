using Microsoft.Azure;

using Microsoft.WindowsAzure.Storage;

using Microsoft.WindowsAzure.Storage.Blob;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;


namespace StorageTest

{

    class Program

    {

        static void Main(string[] args)

        {

            // Parse the connection string and return a reference to the storage account.

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(

                CloudConfigurationManager.GetSetting("StorageConnectionString"));



            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();



            // Retrieve a reference to a container. my_nuget_test_container mynugettestcontainer

            CloudBlobContainer container = blobClient.GetContainerReference("mynugettestcontainer");



            // Create the container if it doesn't already exist.
            try
            {
                container.CreateIfNotExists();
                Console.WriteLine("create container success!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("create container failed! Detail:" + ex.InnerException.ToString());
            }

            // Retrieve reference to a blob named "mynugettestcontainer"
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("mynugettestcontainer");

            using (var fileStream = System.IO.File.OpenRead(@"path\myfile"))
            {
                blockBlob.UploadFromStream(fileStream);
            }
            Console.ReadKey();

        }

    }

}