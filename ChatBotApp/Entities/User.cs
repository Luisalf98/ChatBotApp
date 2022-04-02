using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChatBotApp.Entities
{
  [Index(nameof(Username), IsUnique = true)]
  public class User
  {
    public long Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required, JsonIgnore]
    public string PasswordHash { get; set; }
  }
}
