using ChatBotApp.Data;
using ChatBotTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using ChatBotApp.Entities;

namespace ChatBotTests.Entities
{
  public class UserTests : IClassFixture<DatabaseFixture>, IDisposable
  {
    ApplicationDbContext context;

    public UserTests(DatabaseFixture fixture)
    {
      context = fixture.GetContext();
    }

    public void Dispose()
    {
      context.Dispose();
    }

    [Theory]
    [InlineData("username", null)]
    [InlineData(null, "password")]
    public void ShouldNotSaveInvalidUser(string username, string password)
    {
      context.Add(new User { PasswordHash = password, Username = username });

      Assert.Throws<DbUpdateException>(() => context.SaveChanges());
    }

    [Fact]
    public void ShouldNotSaveUserWithDuplicatedUserame()
    {
      context.Add(new User { Username = "DUPLICATED", PasswordHash = "any_password_hash" });
      context.SaveChanges();

      context.Add(new User { Username = "DUPLICATED", PasswordHash = "any_password_hash" });

      Assert.Throws<DbUpdateException>(() => context.SaveChanges());
    }

    [Fact]
    public void ValidUserIsCreated()
    {
      context.Add(new User { Username = "any_username", PasswordHash = "any_password_hash" });

      context.SaveChanges();

      Assert.NotNull(context.Users.SingleOrDefault(u => u.Username == "any_username"));
    }
  }
}
