namespace AutoHub.Data.Seeding.MandatoryEntityValues
{
    public class Towns
    {
        public static readonly Dictionary<string, List<string>> TownsByRegion = new()
        {
            [Regions.Blagoevgrad] = new()
            {
                "Гоце Делчев",
                "Симитли",
                "Благоевград",
                "Петрич",
                "Разлог",
                "Якоруда",
                "Гърмен",
                "Сандански",
                "Банско",
                "Беласица",
            },

            [Regions.Burgas] = new()
            {
                "Обзор",
                "Елените",
                "Айтос",
                "Бургас",
                "Карнобат",
                "Приморско",
                "Несебър",
                "Поморие",
                "Царево",
                "Созопол",
            },
        };
    }
}
