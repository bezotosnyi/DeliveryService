namespace DeliveryService.BLL.Validator
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using static System.ComponentModel.DataAnnotations.Validator;

    using CustomValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

    /// <summary>
    /// Валидация данных с помощью атрибутов пространства имен System.ComponentModel.DataAnnotations
    /// </summary>
    public static class DataAnnotationsValidator
    {
        public static ValidationResult Validate<T>(T obj) where T : class, new()
        {
            var validationResult = new List<CustomValidationResult>();

            if (TryValidateObject(obj, new ValidationContext(obj), validationResult, true))
                return new ValidationResult(true, "Валидация прошла успешно.");

            var errorMessage = validationResult.Select(x => x.ErrorMessage).Aggregate((x, y) => x + "\n" + y);
            return new ValidationResult(false, errorMessage);

        }
    }
}