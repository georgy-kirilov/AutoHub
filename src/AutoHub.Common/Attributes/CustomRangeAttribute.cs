namespace AutoHub.Common.Attributes
{
    using System.ComponentModel.DataAnnotations;

    using AutoHub.Web.LanguageResources;

    public class CustomRangeAttribute : RangeAttribute
    {
        public CustomRangeAttribute(double minimum, double maximum)
            : base(minimum, maximum)
        {
            this.ErrorMessage = nameof(Resource.InvalidRangeErrorMessageFormat);
        }
    }
}
