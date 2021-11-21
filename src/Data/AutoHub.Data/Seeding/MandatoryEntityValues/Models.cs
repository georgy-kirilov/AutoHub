namespace AutoHub.Data.Seeding.MandatoryEntityValues
{
    public class Models
    {
        public static readonly Dictionary<string, List<string>> ModelsByBrand = new()
        {
            [Brands.Vw] = new() { "Passat", "Golf 3", "Golf 4", "Touran" },
            [Brands.Audi] = new() { "A4", "A6", "A8", "Q5", "Q7", "R8", "RS7" },
            [Brands.Honda] = new() { "Civic", "Accord", "CR-V", "HR-V" },
            [Brands.Peugeot] = new() { "206", "208", "306", "308", "3008" },
            [Brands.Renault] = new() { "Megane", "Clio", "Laguna" },
            [Brands.Opel] = new() { "Astra", "Corsa", "Vectra", "Zafira" },
        };
    }
}
