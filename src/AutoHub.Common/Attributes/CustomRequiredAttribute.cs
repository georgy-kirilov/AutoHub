namespace AutoHub.Common.Attributes
{
    using System.ComponentModel.DataAnnotations;

    using AutoHub.Web.LanguageResources;

    public class CustomRequiredAttribute : RequiredAttribute
    {
        public CustomRequiredAttribute()
        {
            this.ErrorMessage = nameof(Resource.FieldIsRequiredErrorMessageFormat);
        }
    }
}
