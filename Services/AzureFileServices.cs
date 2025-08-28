using System.Security.Cryptography.X509Certificates;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
namespace ST10070824_ABCRetail.Services
{
    public class AzureFileServices
    {
        private readonly string _storageAccountName;
        private readonly string _fileShareName;
        private readonly string _storageAccountKey;
      //  private readonly ShareClient _shareClient;//??

        public AzureFileServices(string storageAccountName, string fileShareName, string storageAccountKey)
        {
            _storageAccountName = storageAccountName;
            _fileShareName = fileShareName;
            _storageAccountKey = storageAccountKey;

       //     string connectionString =
       //    $"DefaultEndpointsProtocol=https;AccountName={storageAccountName};AccountKey={storageAccountKey};EndpointSuffix=core.windows.net";

       //     _shareClient = new ShareClient(connectionString, fileShareName);
       //     _shareClient.CreateIfNotExists();
        }
        public async Task UploadFileAsync(string fileName, string content)
        {
            string shareUri = $"https://{_storageAccountName}.file.core.windows.net/{_fileShareName}"; 
            ShareClient share = new ShareClient(new Uri(shareUri), new Azure.Storage.StorageSharedKeyCredential(_storageAccountName, _storageAccountKey));

            ShareDirectoryClient directory = share.GetRootDirectoryClient();
            ShareFileClient file = directory.GetFileClient(fileName);

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(content);

            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                await file.CreateAsync(stream.Length);
                await file.UploadRangeAsync(new Azure.HttpRange(0, stream.Length), stream);
            }

        }
        /*public async Task DownloadFileAsync(string fileName, string downloadPath)
        {
            ShareDirectoryClient rootDir = _shareClient.GetRootDirectoryClient();
            ShareFileClient fileClient = rootDir.GetFileClient(fileName);

            ShareFileDownloadInfo download = await fileClient.DownloadAsync();

            using (FileStream stream = File.OpenWrite(downloadPath))
            {
                await download.Content.CopyToAsync(stream);
            }
        }*/
        
    }
}
