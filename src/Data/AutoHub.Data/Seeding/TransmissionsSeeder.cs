namespace AutoHub.Data.Seeding
{
    using AutoHub.Common;
    using AutoHub.Data.Models;
    using AutoHub.Data.Seeding.Common;
    using AutoHub.Data.Seeding.MandatoryEntityValues;

    public class TransmissionsSeeder : BaseTypeableModelsSeeder<Transmission>
    {
        public TransmissionsSeeder()
            : base(Extensions.GetConstants<Transmissions, string>())
        {
        }
    }
}
