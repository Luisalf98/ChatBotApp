using AutoMapper;
using ChatBotApp.Data;
using ChatBotApp.Entities;
using ChatBotApp.Models;
using ChatBotApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ChatBotApp.Services
{
  public class ChatRoomService : BaseService, IValidatorService
  {
    private readonly IMapper mapper;

    public ChatRoomService(ApplicationDbContext context, IMapper mapper) : base(context) 
    {
      this.mapper = mapper;
    }

    public ChatRoomViewModel GetById(long chatRoomId)
    {
      return mapper.Map<ChatRoomViewModel>(context.ChatRooms.Find(chatRoomId));
    }

    public IEnumerable<ChatRoomViewModel> GetAll()
    {
      return mapper.ProjectTo<ChatRoomViewModel>(context.ChatRooms).ToList();
    }

    public ChatRoomViewModel Create(ChatRoomCreateModel model)
    {
      var chatRoom = mapper.Map<ChatRoom>(model);
      context.Add(chatRoom);
      context.SaveChanges();
      return mapper.Map<ChatRoomViewModel>(chatRoom);
    }
    
    public bool IsPresent(long chatRoomId)
    {
      return context.ChatRooms.Find(chatRoomId) != null;
    }
  }
}
