using ChatBotApp.Validations;
using System.ComponentModel.DataAnnotations;

namespace ChatBotApp.Models
{
  public class UserModel
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
