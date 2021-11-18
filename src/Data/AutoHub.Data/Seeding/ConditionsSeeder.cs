namespace AutoHub.Data.Seeding
{
    using AutoHub.Common;
    using AutoHub.Data.Models;
    using AutoHub.Data.Seeding.Common;
    using AutoHub.Data.Seeding.MandatoryEntityValues;

    public class ConditionsSeeder : BaseTypeableModelsSeeder<Condition>
    {
        public ConditionsSeeder()
            : base(Extensions.GetConstants<Conditions, string>())
        {
        }
    }
}
