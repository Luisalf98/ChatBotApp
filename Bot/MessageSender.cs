using Microsoft.AspNetCore.SignalR.Client;

namespace Bot
{
  public partial class MessageSender
  {
    private readonly HubConnection connection;
    private readonly MessagePacket message;

    private const string APIKEY = "This_Is_The_Api_Key_For_Authorization_Of_The_Bot__This_Api_Key_Should_Not_Be_Here_But_Just_For_Simplicity";

    public MessageSender(MessagePacket message)
    {
      this.message = message;
      connection = new HubConnectionBuilder()
        .WithUrl("http://localhost:38561/MessagingHub", c =>
        {
          c.Headers.Add("ApiKey", APIKEY);
        })
        .Build();
    }


    public async Task Send()
    {
      try
      {
        await connection.StartAsync();
        await connection.InvokeAsync(
          "SendBotMessage", 
          message.ChatRoomId, 
          $"I am useless right now. Someone sent me this: '{message.Text}'"
        );
        await connection.StopAsync();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}
