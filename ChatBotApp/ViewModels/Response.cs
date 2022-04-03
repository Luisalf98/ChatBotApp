namespace ChatBotApp.ViewModels
{
  public class Response
  {
    public bool Success { get; set; }
    public string Message { get; set; }

    private Response(string message, bool success) => (Message, Success) = (message, success);

    public static Response Successful(string message)
    {
      return new Response(message, true);
    }

    public static Response Failure(string message)
    {
      return new Response(message, false);
    }
  }
}
