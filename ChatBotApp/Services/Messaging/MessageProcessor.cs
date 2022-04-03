using ChatBotApp.Models;
using ChatBotApp.RealTime;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChatBotApp.Services.Messaging
{
  public class MessageProcessor : TextProcessor, IDisposable
  {
    private readonly IHubContext<MessagingHub> hubContext;
    private readonly IServiceScope scope;
    private readonly ChatService chatService;

    public MessageProcessor(
      IServiceProvider services, MessagePacket message
    ) : base(message) 
    {
      hubContext = services.GetRequiredService<IHubContext<MessagingHub>>();
      scope = services.CreateScope();
      chatService = scope.ServiceProvider.GetRequiredService<ChatService>();
    }

    public void Dispose()
    {
      scope.Dispose();
    }

    public override void Process()
    {
      var chatMessage = chatService.Create(message);

      hubContext.Clients.Group(message.ChatRoomId.ToString()).SendAsync(
        "ReceiveMessage", 
        message.ChatRoomId, 
        message.Sender, 
        message.Text,
        chatMessage.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss")
      );
    }
  }
}
