using ChatBotApp.Models;
using System;
using System.Text.RegularExpressions;

namespace ChatBotApp.Services.Messaging
{
  public class TextProcessorFactory
  {
    private readonly IServiceProvider services;

    public TextProcessorFactory(IServiceProvider services)
    {
      this.services = services;
    }

    public TextProcessor Create(MessagePacket message)
    {
      if (Regex.IsMatch(message.Text, "^/((?i)stock(?-i))=(.+)$"))
        return new CommandProcessor(message);
        
      return new MessageProcessor(services, message);
    }
  }
}
