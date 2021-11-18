namespace AutoHub.Data.Seeding
{
    using AutoHub.Common;
    using AutoHub.Data.Models;
    using AutoHub.Data.Seeding.Common;
    using AutoHub.Data.Seeding.MandatoryEntityValues;

    public class ColorsSeeder : BaseNameableModelsSeeder<Color>
    {
        public ColorsSeeder()
            : base(Extensions.GetConstants<Colors, string>())
        {
        }
    }
}
