using ChatBotApp.Services;
using ChatBotApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace ChatBotApp.Authorization
{
  public class ChatRoomAuthorizationHandler : AuthorizationHandler<ChatRoomRequirement, long>
  {

    private readonly UserChatRoomService userChatRoomService;

    public ChatRoomAuthorizationHandler(UserChatRoomService userChatRoomService)
    {
      this.userChatRoomService = userChatRoomService;
    }

    protected override Task HandleRequirementAsync(
      AuthorizationHandlerContext context, ChatRoomRequirement requirement, long chatRoomId
    )
    {
      var userChatRoom = userChatRoomService.GetByUserIdAndChatRoomId(context.User.GetId(), chatRoomId);
      if (userChatRoom != null)
      {
        context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }

  public class ChatRoomRequirement : IAuthorizationRequirement { }
}

