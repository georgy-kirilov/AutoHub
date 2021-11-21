namespace AutoHub.Services
{
    using System.Globalization;

    public class DateTimeService : IDateTimeService
    {
        public IEnumerable<KeyValuePair<string, int>> GetMonthNamesAndNumbers(CultureInfo cultureInfo)
        {
            var monthNamesAndNumbers = new Dictionary<string, int>();

            for (int monthNumber = 1; monthNumber <= 12; monthNumber++)
            {
                string monthName = new DateTime(1999, monthNumber, 1).ToString("MMMM", cultureInfo);
                monthNamesAndNumbers[monthName] = monthNumber;
            }

            return monthNamesAndNumbers;
        }
    }
}
