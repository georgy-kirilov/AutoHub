namespace AutoHub.Common.Exceptions
{
    public class InvalidModelStateException : Exception
    {
        public InvalidModelStateException(IEnumerable<KeyValuePair<string, List<string>>> errorsByPropertyName)
        {
            this.ErrorsByPropertyName = errorsByPropertyName;
        }

        public IEnumerable<KeyValuePair<string, List<string>>> ErrorsByPropertyName { get; }
    }
}
