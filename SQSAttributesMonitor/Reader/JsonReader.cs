using SQSAttributesMonitor.Models;
using System.Text.Json;

namespace SQSAttributesMonitor.Reader
{
    internal class SqsUrlsReader
    {
        internal SqsQueueUrls? SqsQueueUrls { get; set; } = default;

        internal void ReadJson(string jsonFilePath)
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            SqsQueueUrls = JsonSerializer.Deserialize<SqsQueueUrls>(jsonContent);
        }
    }
}
