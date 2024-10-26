using Azure.Storage.Queues;

string queueName = "hello-test";
string connectionString = Environment.GetEnvironmentVariable("QUEUE_CONNECTION_STRING") ?? "DefaultEndpointsProtocol=https;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;QueueEndpoint=http://localhost:10001/devstoreaccount1;";

// Instantiate a QueueClient to create and interact with the queue
QueueClient queueClient = new QueueClient(connectionString, queueName);

do
{
    Console.WriteLine($"Capture: {DateTime.Now}");
    var properties = queueClient.GetProperties();

    int cachedMessagesCount = properties.Value.ApproximateMessagesCount;

    // Display number of messages
    Console.WriteLine($"Number of messages in queue: {cachedMessagesCount}");

    var receivedMessages = await queueClient.ReceiveMessagesAsync(maxMessages: 10);

    if (receivedMessages.Value.Count() == 0)
    {
        Console.WriteLine("Empty message!");
    }

    foreach (var receivedMessage in receivedMessages.Value)
    {
        // Display the message
        Console.WriteLine($"ID: {receivedMessage.MessageId} Message: {receivedMessage.MessageText}");
        // ensure remove it after processing
        await queueClient.DeleteMessageAsync(receivedMessage.MessageId, receivedMessage.PopReceipt);
    }
    Console.WriteLine("------------");
    Thread.Sleep(3000);
} while (true);