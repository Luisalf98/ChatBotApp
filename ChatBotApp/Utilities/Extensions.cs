using System.Security.Claims;

namespace ChatBotApp.Utilities
{
  public static class Extensions
  {
    public static long GetId(this ClaimsPrincipal user)
    {
      long.TryParse(user.FindFirst(ClaimTypes.PrimarySid)?.Value ?? "-1", out long id);

      return id;
    }
  }
}
