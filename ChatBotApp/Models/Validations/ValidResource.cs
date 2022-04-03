using ChatBotApp.Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChatBotApp.Models.Validations
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class ValidResource : ValidationAttribute
  {
    private readonly Type serviceType;
    private ValidationResult InvalidResult => new ValidationResult(ErrorMessage ?? "Resource not found");

    public ValidResource(Type serviceType) => (this.serviceType) = (serviceType);

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var entityService = validationContext.GetService(serviceType) as IValidatorService;
      var entityId = value as long?;
      if (entityId == null)
        return InvalidResult;

      if (!entityService.IsPresent(entityId.Value))
        return InvalidResult;

      return ValidationResult.Success;
    }
  }
}
