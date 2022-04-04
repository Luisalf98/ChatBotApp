using System.Collections.Generic;

namespace ChatBotApp.ViewModels
{
  public class ChatRoomViewModel
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<ChatViewModel> Messages { get; set; }
  }
}
