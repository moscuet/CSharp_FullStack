using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Eshop.Core.src.Validation
{

    public class InternationalPhoneNumberAttribute : ValidationAttribute
    {
        public InternationalPhoneNumberAttribute()
            : base("Phone number must be in international format starting with '+'.")
        {
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string phoneNumber || string.IsNullOrEmpty(phoneNumber))
            {
                return new ValidationResult("Phone number is required.");
            }

            if (!Regex.IsMatch(phoneNumber, @"^\+[1-9]{1}[0-9]{3,14}$"))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}

public class DecimalPrecisionAttribute : ValidationAttribute
{
    private readonly int _precision;

    public DecimalPrecisionAttribute(int precision)
        : base($"The field must not have more than {precision} decimal places.")
    {
        _precision = precision;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("A decimal number is required.");
        }

        if (!(value is decimal decimalValue))
        {
            return new ValidationResult("The value must be a decimal number.");
        }

        if (Math.Round(decimalValue, _precision) != decimalValue)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}


