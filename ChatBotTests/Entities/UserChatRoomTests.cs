using ChatBotApp.Data;
using ChatBotTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using ChatBotApp.Entities;

namespace ChatBotTests.Entities
{
  public class UserChatRoomTests : IClassFixture<UserChatRoomFixture>, IDisposable
  {
    private readonly ApplicationDbContext context;
    private readonly User user;
    private readonly ChatRoom chatRoom;

    public UserChatRoomTests(UserChatRoomFixture fixture)
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
    public void ShouldNotSaveUserChatRoomWithoutUserId()
    {
      context.Add(new UserChatRoom { ChatRoomId = chatRoom.Id});

      Assert.Throws<DbUpdateException>(() => context.SaveChanges());
    }

    [Fact]
    public void ShouldNotSaveUserChatRoomWithoutChatRoomId()
    {
      context.Add(new UserChatRoom { UserId = user.Id });

      Assert.Throws<DbUpdateException>(() => context.SaveChanges());
    }

    [Fact]
    public void ShouldNotSaveUserChatRoomWithDuplicatedUserIdAndChatRoomId()
    {
      context.Add(new UserChatRoom { UserId = user.Id, ChatRoomId = chatRoom.Id });
      context.SaveChanges();

      context.Add(new UserChatRoom { UserId = user.Id, ChatRoomId = chatRoom.Id });

      Assert.Throws<DbUpdateException>(() => context.SaveChanges());
    }

    [Fact]
    public void ValidUserChatRoomIsCreated()
    {
      var user = new User { Username = "NEW_USER", PasswordHash = "ANY_PASSWORD_HASH" };
      var chatRoom = new ChatRoom { Name = "NEW_CHAT_ROOM" };
      context.Add(user);
      context.Add(chatRoom);
      context.SaveChanges();

      context.Add(new UserChatRoom { UserId = user.Id, ChatRoomId = chatRoom.Id });
      context.SaveChanges();

      Assert.NotNull(context.UserChatRooms.SingleOrDefault(u => u.UserId == user.Id && u.ChatRoomId == chatRoom.Id));
    }
  }
}
