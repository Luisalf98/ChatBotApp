using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatBotApp.Entities
{
  public class Chat
  {
    public long Id { get; set; }
    [Required]
    public long UserId { get; set; }
    [Required]
    public long ChatRoomId { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Message { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }    

    public User User { get; set; }
    public ChatRoom ChatRoom { get; set; }
  }
}
