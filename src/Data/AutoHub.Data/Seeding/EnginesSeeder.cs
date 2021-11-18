namespace AutoHub.Data.Seeding
{
    using AutoHub.Data.Models;
    using AutoHub.Data.Seeding.Common;

    public class EnginesSeeder : BaseTypeableModelsSeeder<Engine>
    {
        private static readonly string[] EngineTypes = new[]
        {
            "бензинов",
            "дизелов",
            "хибриден",
            "електрически",
        };

        public EnginesSeeder()
            : base(EngineTypes)
        {
        }
    }
}
