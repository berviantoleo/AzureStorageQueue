using Azure.Storage.Queues;

string queueName = "hello-test";
string connectionString = Environment.GetEnvironmentVariable("QUEUE_CONNECTION_STRING") ?? "DefaultEndpointsProtocol=https;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;QueueEndpoint=http://azurite:10001/devstoreaccount1;";

// Instantiate a QueueClient to create and interact with the queue
QueueClient queueClient = new QueueClient(connectionString, queueName);
Console.WriteLine($"Creating queue: {queueName}");

// Create the queue
await queueClient.CreateAsync();
var i = 0;
do
{
    var name = $"Leo-{i}";
    Console.WriteLine($"Sending: {name}");
    await queueClient.SendMessageAsync(name);
    i += 1;
    Thread.Sleep(1000);
} while (true);