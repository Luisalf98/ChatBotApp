using ChatBotApp.Data;
using ChatBotTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using ChatBotApp.Entities;

namespace ChatBotTests.Entities
{
  public class ChatTests : IClassFixture<UserChatRoomFixture>, IDisposable
  {
    private readonly ApplicationDbContext context;
    private readonly User user;
    private readonly ChatRoom chatRoom;

    public ChatTests(UserChatRoomFixture fixture)
    {
      context = fixture.GetContext();
      user = fixture.User;
      chatRoom = fixture.ChatRoom;
    }

    public void Dispose()
    {
      context.Dispose();
    }

    [Fact]
    public void ShouldNotSaveChatWithoutUserId()
    {
      context.Add(new Chat { ChatRoomId = chatRoom.Id, Message = "ANY MESSAGE"});

      Assert.Throws<DbUpdateException>(() => context.SaveChanges());
    }

    [Fact]
    public void ShouldNotSaveChatWithoutChatRoomId()
    {
      context.Add(new Chat { UserId = user.Id, Message = "ANY MESSAGE" });

      Assert.Throws<DbUpdateException>(() => context.SaveChanges());
    }

    [Fact]
    public void ShouldNotSaveChatWithoutMessage()
    {
      context.Add(new Chat { UserId = user.Id, ChatRoomId = chatRoom.Id });

      Assert.Throws<DbUpdateException>(() => context.SaveChanges());
    }

    [Fact]
    public void ValidChatIsCreated()
    {
      context.Add(new Chat { UserId = user.Id, ChatRoomId = chatRoom.Id, Message = "SOME MESSAGE" });
      context.SaveChanges();

      Assert.NotNull(context.Chats.SingleOrDefault(u => u.Message == "SOME MESSAGE"));
    }
  }
}
