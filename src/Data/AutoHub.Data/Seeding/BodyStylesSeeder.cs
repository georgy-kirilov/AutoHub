namespace AutoHub.Data.Seeding
{
    using AutoHub.Common;
    using AutoHub.Data.Models;
    using AutoHub.Data.Seeding.Common;
    using AutoHub.Data.Seeding.MandatoryEntityValues;

    public class BodyStylesSeeder : BaseNameableModelsSeeder<BodyStyle>
    {
        public BodyStylesSeeder()
            : base(Extensions.GetConstants<BodyStyles, string>())
        {
        }
    }
}
