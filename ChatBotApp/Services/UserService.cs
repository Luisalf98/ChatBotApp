using ChatBotApp.Data;
using ChatBotApp.Entities;
using ChatBotApp.Models;
using ChatBotApp.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChatBotApp.Services
{
  public class UserService : BaseService
  {
    public UserService(ApplicationDbContext context) : base(context)
    {
    }

    public User GetByUsername(string username, params string[] includes)
    {
      username = (username ?? "").Trim().ToUpper();
      IQueryable<User> users = context.Users;
      foreach(var relationship in includes)
      {
        users = users.Include(relationship);
      }

      return users.SingleOrDefault(u => u.Username.Trim().ToUpper() == username);
    }
  }
}
