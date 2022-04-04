using ChatBotApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChatBotApp.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<ChatRoom> ChatRooms { get; set; }
    public DbSet<UserChatRoom> UserChatRooms { get; set; }
    public DbSet<Chat> Chats { get; set; } 
  }
}
