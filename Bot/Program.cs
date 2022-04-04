using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Bot
{
  public class Receive
  {

    private static string QueueName = "ChatBotApp";

    private static void InitQueue(IModel channel)
    {
      channel.QueueDeclare(queue: QueueName,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
    }

    private static void SubscribeToQueue(IModel channel)
    {
      var consumer = new EventingBasicConsumer(channel);
      consumer.Received += async (model, ea) =>
      {
        var body = ea.Body.ToArray();
        var json = Encoding.UTF8.GetString(body);
        var message = JsonConvert.DeserializeObject<MessagePacket>(json);
        Console.WriteLine(" [x] Received {0}, {1}, {2}", message?.Text, message?.Sender, message?.ChatRoomId);
        await new MessageSender(message ?? new()).Send();
      };
      channel.BasicConsume(queue: QueueName,
                           autoAck: true,
                           consumer: consumer);
    }

    public static void Main()
    {
      var factory = new ConnectionFactory() { HostName = "localhost" };
      using (var connection = factory.CreateConnection())
      using (var channel = connection.CreateModel())
      {
        InitQueue(channel);

        SubscribeToQueue(channel);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
      }
    }
  }
}
