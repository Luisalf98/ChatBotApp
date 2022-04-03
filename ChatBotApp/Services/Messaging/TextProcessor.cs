using ChatBotApp.Models;

namespace ChatBotApp.Services.Messaging
{
  public abstract class TextProcessor
  {
    protected readonly MessagePacket message;

    public TextProcessor(MessagePacket message) => this.message = message;

    public abstract void Process();
  }
}
