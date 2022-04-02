using ChatBotApp.Data;
using ChatBotApp.Entities;
using System.Linq;

namespace ChatBotApp.Utilities
{
  public static class DbInitializer
  {
    public static void Initialize(ApplicationDbContext context)
    {
      context.Database.EnsureCreated();

      string[] usernames = { "user1", "user2", "user3", "user4", "user5" };

      foreach (var username in usernames)
      {
        var user = context.Users.SingleOrDefault(r => r.Username == username);
        if (user == null)
        {
          user = new User { Username = username };
          user.PasswordHash = PasswordHasherUtil<User>.HashPassword(user, "password123");
          context.Add(user);
        }
      }
      context.SaveChanges();
    }
  }
}
