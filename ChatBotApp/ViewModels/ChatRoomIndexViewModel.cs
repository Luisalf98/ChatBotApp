using System.Collections.Generic;

namespace ChatBotApp.ViewModels
{
  public class ChatRoomIndexViewModel
  {
    public IEnumerable<ChatRoomViewModel> UserChats { get; set; }
    public IEnumerable<ChatRoomViewModel> OtherChats { get; set; }
  }
}
