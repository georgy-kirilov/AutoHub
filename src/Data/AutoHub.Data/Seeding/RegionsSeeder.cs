namespace AutoHub.Data.Seeding
{
    using AutoHub.Common;
    using AutoHub.Data.Models;
    using AutoHub.Data.Seeding.Common;
    using AutoHub.Data.Seeding.MandatoryEntityValues;

    public class RegionsSeeder : BaseNameableModelsSeeder<Region>
    {
        public RegionsSeeder()
            : base(Extensions.GetConstants<Regions, string>())
        {
        }
    }
}
