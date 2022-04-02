using System.ComponentModel.DataAnnotations;
using ChatBotApp.Models.Validations;

namespace ChatBotApp.Models
{
  public class UserLoginModel
  {
    public class UserData
    {
      [Required(AllowEmptyStrings = false)]
      public string Username { get; set; }
      [Required(AllowEmptyStrings = false)]
      public string Password { get; set; }
    }

    [ValidUser]
    public UserData User { get; set; }
    
  }
}
