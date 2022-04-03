using ChatBotApp.Models;

namespace ChatBotApp.Services.Messaging
{
  public class CommandProcessor : TextProcessor
  {
    public CommandProcessor(MessagePacket message) : base(message) { }

    public override void Process()
    {
      
    }
  }
}
