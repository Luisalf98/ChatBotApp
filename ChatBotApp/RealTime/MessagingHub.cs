using ChatBotApp.Models;
using ChatBotApp.Services.Messaging;
using ChatBotApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatBotApp.RealTime
{
  [Authorize]
  public class MessagingHub : Hub
  {
    private readonly IAuthorizationService authorizationService;
    private readonly TextProcessorFactory textProcessorFactory;
    
    public MessagingHub(
      IAuthorizationService authorizationService, 
      TextProcessorFactory textProcessorFactory
    )
    {
      this.authorizationService = authorizationService;
      this.textProcessorFactory = textProcessorFactory;
    }

    private async Task<bool> IsUserAllowed(long chatRoomId)
    {
      var authorizationResult = await authorizationService.AuthorizeAsync(
        Context.User, chatRoomId, "ChatRoomPolicy"
      );
      if (authorizationResult.Succeeded) return true;

      await Clients.Caller.SendAsync(
        "HandleError", "You are not authorized to send messages to this chat room."
      );
        
      return false;
    }
    
    private MessagePacket messagePacket(long chatRoomId, string message)
    {
      return new MessagePacket
      {
        ChatRoomId = chatRoomId,
        Text = message,
        Sender = Context.User.Identity.Name,
        SenderId = Context.User.GetId()
      };
    }

    public async Task SendMessage(long chatRoomId, string text)
    {
      if (string.IsNullOrWhiteSpace(text)) return;
      if (!(await IsUserAllowed(chatRoomId))) return;

      var message = messagePacket(chatRoomId, text);
      textProcessorFactory.Create(message).Process();
    }

    public async Task SubscribeToChatRoom(long chatRoomId)
    {
      if (!(await IsUserAllowed(chatRoomId))) return;

      await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId.ToString());

      await Clients.Caller.SendAsync("SubscribedSuccessfully", chatRoomId);
    }
  }
}
