using System.ComponentModel.DataAnnotations;

public static class EntityValidator
{
    public static void ValidateEntity(object entity)
    {
        var validationContext = new ValidationContext(entity, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(entity, validationContext, validationResults, validateAllProperties: true))
        {
            var exceptionMessage = string.Join("; ", validationResults.Select(result => result.ErrorMessage));
            throw new ValidationException(exceptionMessage);
        }
    }
}
