using AutoMapper;
using ChatBotApp.Entities;
using ChatBotApp.Models;
using ChatBotApp.ViewModels;
using System;

namespace ChatBotApp.Configuration
{
  public static class ModelsMapperConfigurator
  {
    public static void Configure(IMapperConfigurationExpression config)
    {
      config.CreateMap<ChatRoomCreateModel, ChatRoom>();
      config.CreateMap<ChatRoom, ChatRoomViewModel>();
      config.CreateMap<UserChatRoomCreateModel, UserChatRoom>();
      config.CreateMap<MessagePacket, Chat>()
            .ForMember("UserId", c => c.MapFrom(m => m.SenderId))
            .ForMember("Message", c => c.MapFrom(m => m.Text))
            .ForMember("CreatedAt", c => c.MapFrom(m => DateTime.Now));
      config.CreateMap<Chat, ChatViewModel>()
            .ForMember("Username", c => c.MapFrom(m => m.User.Username));
    }
  }
}
