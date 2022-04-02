using ChatBotApp.Entities;
using ChatBotApp.Models;
using ChatBotApp.Services;
using ChatBotApp.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChatBotApp.Validations
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class ValidUser : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var userService = validationContext.GetService(typeof(UserService)) as UserService;
      var model = value as UserModel.UserData;

      var user = userService.GetByUsername(model.Username);
      if (user == null)
        return InvalidResult;

      if (!PasswordHasherUtil<User>.VerifyPassword(user, user.PasswordHash, model.Password))
        return InvalidResult;

      return ValidationResult.Success;
    }

    private static ValidationResult invalidResult;
    private static ValidationResult InvalidResult { 
      get {
        if (invalidResult == null)
          invalidResult = new ValidationResult("Invalid username or password");

        return invalidResult;
      }
    }
  }
}
