namespace AutoHub.Common
{
    using System.Reflection;

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
    }
}
