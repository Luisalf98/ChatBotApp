using AutoMapper;
using ChatBotApp.Data;
using ChatBotApp.Entities;
using ChatBotApp.Models;
using ChatBotApp.ViewModels;
using System.Linq;

namespace ChatBotApp.Services
{
  public class UserChatRoomService : BaseService
  {
    private readonly IMapper mapper;

    public UserChatRoomService(ApplicationDbContext context, IMapper mapper) : base(context) 
    {
      this.mapper = mapper;
    }

    public void Create(UserChatRoomCreateModel model)
    {
      var userChatRoom = mapper.Map<UserChatRoom>(model);
      context.Add(userChatRoom);
      context.SaveChanges();
    }

    public IQueryable<UserChatRoom> GetAllByUserId(long userId)
    {
      return context.UserChatRooms.Where(u => u.UserId == userId);
    }

    public UserChatRoom GetByUserIdAndChatRoomId(long userId, long chatRoomId)
    {
      return context.UserChatRooms.FirstOrDefault(u =>
        u.ChatRoomId == chatRoomId && u.UserId == userId
      );
    }
  }
}
