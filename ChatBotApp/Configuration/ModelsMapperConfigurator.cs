using AutoMapper;
using ChatBotApp.Entities;
using ChatBotApp.Models;
using ChatBotApp.ViewModels;

namespace ChatBotApp.Configuration
{
  public static class ModelsMapperConfigurator
  {
    public static void Configure(IMapperConfigurationExpression config)
    {
      config.CreateMap<ChatRoomCreateModel, ChatRoom>();
      config.CreateMap<ChatRoom, ChatRoomViewModel>();
    }
  }
}
