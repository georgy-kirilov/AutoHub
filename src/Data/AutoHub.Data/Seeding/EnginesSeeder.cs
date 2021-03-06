namespace AutoHub.Data.Seeding
{
    using AutoHub.Common;
    using AutoHub.Data.Models;
    using AutoHub.Data.Seeding.Common;
    using AutoHub.Data.Seeding.MandatoryEntityValues;

    public class EnginesSeeder : BaseTypeableModelsSeeder<Engine>
    {
        public EnginesSeeder()
            : base(Extensions.GetConstants<Engines, string>())
        {
        }
    }
}
