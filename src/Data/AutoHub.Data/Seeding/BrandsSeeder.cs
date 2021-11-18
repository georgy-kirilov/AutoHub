namespace AutoHub.Data.Seeding
{
    using AutoHub.Common;
    using AutoHub.Data.Models;
    using AutoHub.Data.Seeding.Common;
    using AutoHub.Data.Seeding.MandatoryEntityValues;

    public class BrandsSeeder : BaseNameableModelsSeeder<Brand>
    {
        public BrandsSeeder()
            : base(Extensions.GetConstants<Brands, string>())
        {
        }
    }
}
