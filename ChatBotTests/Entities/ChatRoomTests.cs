using ChatBotApp.Data;
using ChatBotTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using ChatBotApp.Entities;

namespace ChatBotTests.Entities
{
  public class ChatRoomTests : IClassFixture<DatabaseFixture>, IDisposable
  {
    ApplicationDbContext context;

    public ChatRoomTests(DatabaseFixture fixture)
    {
      context = fixture.GetContext();
    }

    public void Dispose()
    {
      context.Dispose();
    }

    [Fact]
    public void ShouldNotSaveInvalidChatRoom()
    {
      context.Add(new ChatRoom());

      Assert.Throws<DbUpdateException>(() => context.SaveChanges());
    }

    [Fact]
    public void ShouldNotSaveChatRoomWithDuplicatedName()
    {
      context.Add(new ChatRoom { Name = "Duplicated" });
      context.SaveChanges();

      context.Add(new ChatRoom { Name = "Duplicated" });

      Assert.Throws<DbUpdateException>(() => context.SaveChanges());
    }

    [Fact]
    public void ValidChatRoomIsCreated()
    {
      context.Add(new ChatRoom { Name = "any_chatroom_name" });

      context.SaveChanges();

      Assert.NotNull(context.ChatRooms.SingleOrDefault(u => u.Name == "any_chatroom_name"));
    }
  }
}
