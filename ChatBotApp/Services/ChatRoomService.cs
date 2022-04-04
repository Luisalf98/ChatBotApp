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
    private readonly UserChatRoomService userChatRoomService;

    public ChatRoomService(
      ApplicationDbContext context, 
      IMapper mapper,
      UserChatRoomService userChatRoomService
    ) : base(context) 
    {
      this.mapper = mapper;
      this.userChatRoomService = userChatRoomService;
    }

    public ChatRoomViewModel GetById(long chatRoomId)
    {
      return mapper.Map<ChatRoomViewModel>(context.ChatRooms.Find(chatRoomId));
    }

    public IEnumerable<ChatRoomViewModel> GetAll()
    {
      return mapper.ProjectTo<ChatRoomViewModel>(context.ChatRooms).ToList();
    }

    public IEnumerable<ChatRoomViewModel> GetOthers(long userId)
    {
      var chatRoomIds = GetIdsByUserId(userId);
      var chatRooms = context.ChatRooms.Where(c =>
        !chatRoomIds.Contains(c.Id)
      );

      return mapper.ProjectTo<ChatRoomViewModel>(chatRooms).ToList();
    }

    public IEnumerable<ChatRoomViewModel> GetAllByUserId(long userId)
    {
      var chatRoomIds = GetIdsByUserId(userId);
      var chatRooms = context.ChatRooms.Where(c =>
        chatRoomIds.Contains(c.Id)
      );

      return mapper.ProjectTo<ChatRoomViewModel>(chatRooms).ToList();
    }

    public ChatRoomViewModel Create(ChatRoomCreateModel model)
    {
      var chatRoom = mapper.Map<ChatRoom>(model);
      context.Add(chatRoom);
      context.SaveChanges();
      userChatRoomService.Create(new UserChatRoomCreateModel
      {
        UserId = model.UserId,
        ChatRoomId = chatRoom.Id
      });
      
      return mapper.Map<ChatRoomViewModel>(chatRoom);
    }
    
    public bool IsPresent(long chatRoomId)
    {
      return context.ChatRooms.Find(chatRoomId) != null;
    }

    public IQueryable<long> GetIdsByUserId(long userId)
    {
      return userChatRoomService.GetAllByUserId(userId)
                                .Select(u => u.ChatRoomId);
    } 
  }
}
