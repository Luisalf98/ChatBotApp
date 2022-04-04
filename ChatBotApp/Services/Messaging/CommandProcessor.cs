using ChatBotApp.Models;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

namespace ChatBotApp.Services.Messaging
{
  public class CommandProcessor : TextProcessor
  {
    private string QueueName = "ChatBotApp";

    public CommandProcessor(MessagePacket message) : base(message) { }

    private void InitQueue(IModel channel)
    {
      channel.QueueDeclare( queue: QueueName,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
    }

    private void SendMessage(IModel channel)
    {
      var json = JsonConvert.SerializeObject(message);
      var body = Encoding.UTF8.GetBytes(json);
      channel.BasicPublish( exchange: "",
                            routingKey: QueueName,
                            basicProperties: null,
                            body: body);
    }

    public override void Process()
    {
      var factory = new ConnectionFactory() { HostName = "localhost" };
      using (var connection = factory.CreateConnection())
      using (var channel = connection.CreateModel())
      {
        InitQueue(channel);

        SendMessage(channel);
      }
    }
  }
}
