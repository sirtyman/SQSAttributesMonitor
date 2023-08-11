using System.Text;

namespace SQSAttributesMonitor.Models
{
    public class SqsQueueUrls
    {
        public string[] Urls { get; set; } = Array.Empty<string>();

        public override string ToString()
        {
            StringBuilder allUrls = new();

            foreach (string url in Urls)
            {
                allUrls.Append(url);
                allUrls.Append('\n');
            }

            return allUrls.ToString();
        }
    }
}
