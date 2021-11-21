namespace AutoHub.Common.Attributes
{
    using System.ComponentModel.DataAnnotations;

    using AutoHub.Web.LanguageResources;

    public class HorsePowersRangeAttribute : ValidationAttribute
    {
        private readonly int minHorsePowers;
        private readonly int maxHorsePowers;

        public HorsePowersRangeAttribute(int minHorsePowers, int maxHorsePowers)
        {
            this.minHorsePowers = minHorsePowers;
            this.maxHorsePowers = maxHorsePowers;
        }

        public override bool IsValid(object value)
        {
            if (value is not int)
            {
                string message = $"{nameof(HorsePowersRangeAttribute)} can only be applied on properties of type int";
                throw new ArgumentException(message);
            }

            int horsePowers = (int)value;

            this.ErrorMessage = string.Format(Resource.HorsePowersOutOfRange, this.minHorsePowers, this.maxHorsePowers);

            return horsePowers >= this.minHorsePowers && horsePowers <= this.maxHorsePowers;
        }
    }
}
