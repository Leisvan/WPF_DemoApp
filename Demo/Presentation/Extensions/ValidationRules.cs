using Demo.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Demo.Presentation.Extensions
{
    public class NotEmptyStringValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? string.Empty).ToString())
                ? new ValidationResult(false, Resources.FieldIsRequired)
                : ValidationResult.ValidResult;
        }
    }
    
    public class DoubleValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (double.TryParse(value.ToString(), out double num))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Value must be a number");
        }
    }
    public class IntegerValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse(value.ToString(), out _))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Value must be a number in range");
        }
    }
    public class AgeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse(value.ToString(), out int num))
            {
                if (num > 0)
                {
                    return ValidationResult.ValidResult;
                }

            }
            return new ValidationResult(false, Resources.ValidAge);
        }
    }
}
