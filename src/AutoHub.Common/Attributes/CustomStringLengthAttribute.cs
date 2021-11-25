namespace AutoHub.Common.Attributes
{
    using System.ComponentModel.DataAnnotations;

    using AutoHub.Web.LanguageResources;

    public class CustomStringLengthAttribute : StringLengthAttribute
    {
        public CustomStringLengthAttribute(int maximumLength)
            : base(maximumLength)
        {
            this.ErrorMessage = nameof(Resource.StringExceedMaxLengthErrorMessageFormat);
        }

        public CustomStringLengthAttribute(int minimumLength, int maximumLength)
            : base(maximumLength)
        {
            this.MinimumLength = minimumLength;
            this.ErrorMessage = nameof(Resource.StringLengthErrorMessageFormat);
        }
    }
}
