using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChatBotApp.Entities
{
  [Index(nameof(ChatRoomId), nameof(UserId), IsUnique = true)]
  public class UserChatRoom
  {
    public long Id { get; set; }
    [Required]
    public long ChatRoomId { get; set; }
    [Required]
    public long UserId { get; set; }

    public ChatRoom ChatRoom { get; set; }
    public User User { get; set; }
  }
}
