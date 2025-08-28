using ST10070824_ABCRetail.Models;
using Azure.Storage.Blobs;
using ST10070824_ABCRetail.Services;

namespace ST10070824_ABCRetail
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var blobServiceClient = new BlobServiceClient(builder.Configuration["AzureBlobStorage:ConnectionString"]);
             
            var connectionString = builder.Configuration.GetConnectionString("AzureQueueConnection");

            var queueName = builder.Configuration["AzureQueueSettings:QueueName"];

            builder.Services.AddSingleton(new AzureQueueService(connectionString,queueName));
            builder.Services.AddSingleton(blobServiceClient);
            builder.Services.AddScoped<AzureBlobServices>();

            
            string storageAccountName = builder.Configuration["AzureFileStorage:AccountName"];
            string fileShareName = builder.Configuration["AzureFileStorage:FileShareName"];
            string storageAccountKey = builder.Configuration["AzureFileStorage:AccountKey"];

        //    var connectionString = $"DefaultEndpointsProtocol=https;AccountName={storageAccountName};AccountKey={storageAccountKey};EndpointSuffix=core.windows.net";
            builder.Services.AddSingleton(new AzureQueueService(connectionString, queueName));

          //  builder.Services.AddSingleton(new BlobServiceClient(connectionString));

            builder.Services.AddSingleton(new AzureFileServices(storageAccountName, fileShareName, storageAccountKey));

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
               // .WithStaticAssets();

            app.Run();
        }
    }
}
