namespace AutoHub.Data.Seeding
{
    using AutoHub.Data.Models;
    using AutoHub.Data.Seeding.Common;

    public class BodyStylesSeeder : BaseNameableModelsSeeder<BodyStyle>
    {
        private static readonly string[] BodyStyleNames = new[]
        {
            "ван",
            "седан",
            "хечбек",
            "комби",
            "купе",
            "кабрио",
            "джип",
            "пикап",
            "миниван",
            "стреч лимузина",
            "бус/минибус",
        };

        public BodyStylesSeeder()
            : base(BodyStyleNames)
        {
        }
    }
}
