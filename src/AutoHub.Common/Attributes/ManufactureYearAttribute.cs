namespace AutoHub.Common.Attributes
{
    using System.ComponentModel.DataAnnotations;

    using AutoHub.Web.LanguageResources;

    public class ManufactureYearAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not int)
            {
                string message = $"{nameof(ManufactureYearAttribute)} can only be applied on properties of type int";
                throw new ArgumentException(message);
            }

            int year = (int)value;
            int minYear = ValidationConstraints.MinManufactureYear;
            int maxYear = DateTime.UtcNow.Year;

            this.ErrorMessage = string.Format(
                Resource.InvalidRangeErrorMessageFormat, Resource.ManufactureYearDisplayName, minYear, maxYear);

            return year >= minYear && year <= maxYear;
        }
    }
}
