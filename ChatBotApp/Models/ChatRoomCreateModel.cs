using System.ComponentModel.DataAnnotations;

namespace ChatBotApp.Models
{
  public class ChatRoomCreateModel
  {
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }
    public long UserId { get; set; }
  }
}
