using AutoMapper;
using ChatBotApp.Data;
using ChatBotApp.Entities;
using ChatBotApp.Models;
using ChatBotApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChatBotApp.Services
{
  public class ChatService : BaseService
  {
    private readonly IMapper mapper;

    public ChatService(
      ApplicationDbContext context, IMapper mapper
    ) : base(context) 
    {
      this.mapper = mapper;
    }

    public IQueryable<ChatViewModel> GetAllByChatRoomId(long chatRoomId)
    {
      return mapper.ProjectTo<ChatViewModel>(
        context.Chats.Where(u => u.ChatRoomId == chatRoomId)
                     .OrderByDescending(c => c.CreatedAt)
      );
    }

    public ChatViewModel Create(MessagePacket model)
    {
      var chat = mapper.Map<Chat>(model);
      context.Add(chat);
      context.SaveChanges();

      return mapper.Map<ChatViewModel>(chat);
    }
  }
}
