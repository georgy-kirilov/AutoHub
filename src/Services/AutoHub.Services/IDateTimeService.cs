namespace AutoHub.Services
{
    using System.Globalization;

    public interface IDateTimeService
    {
        IEnumerable<KeyValuePair<string, int>> GetMonthNamesAndNumbers(CultureInfo cultureInfo);
    }
}
