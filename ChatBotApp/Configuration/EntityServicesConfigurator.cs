using ChatBotApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBotApp.Configuration
{
  public static class EntityServicesConfigurator
  {
    public static void AddEntityServices(this IServiceCollection services)
    {
      services.AddScoped<UserService>();
      services.AddScoped<ChatRoomService>();
      services.AddScoped<UserChatRoomService>();
      services.AddScoped<ChatService>();
    }
  }
}
