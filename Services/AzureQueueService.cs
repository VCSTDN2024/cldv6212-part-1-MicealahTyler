using System.Text.Json;
using Azure;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;

namespace ST10070824_ABCRetail.Models
{
    public class AzureQueueService
    {
        private readonly QueueClient _queueClient;
        
        public AzureQueueService(String connectionString, string queueName)
        {
            _queueClient = new QueueClient(connectionString, queueName);
            _queueClient.CreateIfNotExists();
        }
        public async Task SendMessageAsync<T>(T message)
        {
            if (_queueClient.Exists())
            {
                string messageText = JsonSerializer.Serialize(message);

                string base64Message = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(messageText));
                await _queueClient.SendMessageAsync(base64Message);


            }
        }
       
        public async Task<string?> ReceiveMessageAsync()
        {
            if (_queueClient.Exists())
            {
                var response = await _queueClient.ReceiveMessageAsync();
                if (response.Value != null)
                {
                    string base64Message = response.Value.MessageText;
                    string decodedMessage = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64Message));
                    return decodedMessage;
                }
            }
            return null;
        }

    }
}
