namespace AutoHub.Common
{
    using System.Reflection;

    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public static class Extensions
    {
        public static IEnumerable<TOut> GetConstants<T, TOut>()
        {
            return typeof(T)
                    .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Where(x => x.IsLiteral && !x.IsInitOnly && x.FieldType.Equals(typeof(TOut)))
                    .Select(x => (TOut)x.GetValue(null))
                    .ToList();
        }

        public static void AddErrors(
            this ModelStateDictionary modelState,
            IEnumerable<KeyValuePair<string, List<string>>> errorsByPropertyName)
        {
            foreach (var property in errorsByPropertyName)
            {
                foreach (string error in property.Value)
                {
                    modelState.AddModelError(property.Key, error);
                }
            }
        }
    }
}
