using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatBotApp.Entities
{
  [Index(nameof(Name), IsUnique = true)]
  public class ChatRoom
  {
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
  }
}
