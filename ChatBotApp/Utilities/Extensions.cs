using System.Security.Claims;

namespace ChatBotApp.Utilities
{
  public static class Extensions
  {
    public static long GetId(this ClaimsPrincipal user)
    {
      return long.Parse(user.FindFirst(ClaimTypes.PrimarySid).Value);
    }
  }
}
