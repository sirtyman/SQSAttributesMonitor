using Amazon.Runtime;
using Amazon.SQS;
using SQSAttributesMonitor.Monitor;
using SQSAttributesMonitor.Reader;

static string? GetPathToJson()
{
    string jsonFile = "sqs-queues.json";
    string currentDirectory = Directory.GetCurrentDirectory();
    string? currentPath = currentDirectory;

    while (!File.Exists(Path.Combine(currentPath!, jsonFile))) // Replace with your project file name
    {
        currentPath = Directory.GetParent(currentPath!)?.FullName;
    }

    return Path.Combine(currentPath!, jsonFile);
}

string jsonFilePath = GetPathToJson();

SqsUrlsReader reader = new SqsUrlsReader();
reader.ReadJson(jsonFilePath);

Console.WriteLine(reader.SqsQueueUrls);

BasicAWSCredentials awsCreds = new ("ignore", "ignore");
var config = new AmazonSQSConfig
{
    ServiceURL = "http://localhost:4566"
};
AmazonSQSClient amazonSQSClient = new (awsCreds, config);

List<Task> tasksList = new();
while (true)
{
    foreach (string url in reader.SqsQueueUrls.Urls)
    {
        tasksList.Add(SQSMonitor.ReadSQSApproxNumberOfMessages(amazonSQSClient, url));
    }

    await Task.WhenAll(tasksList);

    Console.WriteLine("\n#############################\n");

    tasksList.Clear();
}
