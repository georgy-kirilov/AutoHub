namespace AutoHub.Common
{
    public static class ValidationConstraints
    {
        public const int MinManufactureYear = 1886;

        public const int MaxHorsePowers = 1500;

        public const int MaxDescriptionLength = 3000;

        public const int MaxTitleLength = 100;

        public const int MinTitleLength = 5;

        public const double MaxPrice = 30_400_000;

        public const double MinPrice = 0;

        public const int MaxKilometrage = 5_000_000;
    }
}
