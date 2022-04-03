namespace ChatBotApp.Models
{
  public class MessagePacket
  {
    public string Text { get; set; }
    public string Sender { get; set; }
    public long SenderId { get; set; }
    public long ChatRoomId { get; set; }
  }
}
