using ChatBotApp.Models.Validations;
using ChatBotApp.Services;
using System.ComponentModel.DataAnnotations;

namespace ChatBotApp.Models
{
  public class UserChatRoomCreateModel
  {
    [Required]
    [ValidResource(typeof(ChatRoomService))]
    public long ChatRoomId { get; set; }
    public long UserId { get; set; }
  }
}
