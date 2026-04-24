using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Validation
{
    public class CzechPhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var phone = value.ToString() ?? "";

            var cleaned = phone.Replace(" ", "");

            if (cleaned.StartsWith("+420"))
                cleaned = cleaned.Substring(4);
            else if (cleaned.StartsWith("420"))
                cleaned = cleaned.Substring(3);

            if (cleaned.Length != 9 || !cleaned.All(char.IsDigit))
            {
                return new ValidationResult("Neplatné české telefonní číslo");
            }

            return ValidationResult.Success;
        }
    }
}
