using Azure;
using Azure.Data.Tables;

namespace ST10070824_ABCRetail.Models
{
    public class Customer : ITableEntity
    {
        
        public string PartitionKey { get; set; } = "customerProfile";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

       
        public string Name { get; set; }
        public string Email { get; set; }
        public string phoneNumber { get; set; }

    }
}
