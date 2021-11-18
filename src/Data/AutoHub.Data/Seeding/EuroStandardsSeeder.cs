namespace AutoHub.Data.Seeding
{
    using AutoHub.Common;
    using AutoHub.Data.Models;
    using AutoHub.Data.Seeding.Common;
    using AutoHub.Data.Seeding.MandatoryEntityValues;

    public class EuroStandardsSeeder : BaseTypeableModelsSeeder<EuroStandard>
    {
        public EuroStandardsSeeder()
            : base(Extensions.GetConstants<EuroStandards, string>())
        {
        }
    }
}
