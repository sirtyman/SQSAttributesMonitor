using Amazon;
using Amazon.SQS;
using Amazon.Runtime;
using Amazon.SQS.Model;

namespace SQSAttributesMonitor.Monitor
{
    public class SQSMonitor
    {
        public static async Task ReadSQSApproxNumberOfMessages(AmazonSQSClient amazonSQSClient, string sqsQueueUrl)
        {
            var request = new GetQueueAttributesRequest
            {
                QueueUrl = sqsQueueUrl,
                AttributeNames = new List<string> { "ApproximateNumberOfMessages" }
            };

            GetQueueAttributesResponse response = await amazonSQSClient.GetQueueAttributesAsync(request);

            if (response.Attributes.TryGetValue("ApproximateNumberOfMessages", out string approximateNumberOfMessagesString) &&
                int.TryParse(approximateNumberOfMessagesString, out int approximateNumberOfMessages) &&
                response.ApproximateNumberOfMessages > 0)
            {
                Console.WriteLine($"Approximate number of messages in the queue {sqsQueueUrl}: {approximateNumberOfMessages}");
            }
        }
    }
}
