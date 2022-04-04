using ChatBotApp.Entities;
using System.Linq;

namespace ChatBotTests.Fixtures
{
  public class UserChatRoomFixture : DatabaseFixture
  {
    public User User { get; }
    public ChatRoom ChatRoom { get; }

    public UserChatRoomFixture() : base()
    {
      using (var context = GetContext())
      {
        User = context.Users.SingleOrDefault(r => r.Username == "ANY_USER");
        if (User == null)
        {
          User = new User
          {
            Username = "ANY_USER",
            PasswordHash = "ANY_PASSWORD_HASH"
          };
          context.Add(User);
          context.SaveChanges();
        }

        ChatRoom = context.ChatRooms.FirstOrDefault(r => r.Name == "ANY_NAME");
        if (ChatRoom == null)
        {
          ChatRoom = new ChatRoom
          {
            Name = "ANY_NAME"
          };
          context.Add(ChatRoom);
          context.SaveChanges();
        }
      }
    }
  }
}
