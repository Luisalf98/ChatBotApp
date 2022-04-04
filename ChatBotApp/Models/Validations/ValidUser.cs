using ChatBotApp.Entities;
using ChatBotApp.Services;
using ChatBotApp.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChatBotApp.Models.Validations
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class ValidUser : ValidationAttribute
  {
    private ValidationResult InvalidResult { get { 
        return new ValidationResult(ErrorMessage ?? "Invalid username or password");
      } 
    }
    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var userService = validationContext.GetService(typeof(UserService)) as UserService;
      var model = value as UserLoginModel.UserData;

      var user = userService.GetByUsername(model.Username);
      if (user == null)
        return InvalidResult;

      if (!PasswordHasherUtil<User>.VerifyPassword(user, user.PasswordHash, model.Password))
        return InvalidResult;

      model.Id = user.Id;
      return ValidationResult.Success;
    }
  }
}
