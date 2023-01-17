using ShoppingCart.Entities.Attributes;
using ShoppingCart.Entities.Generics;
using System.Reflection;

namespace ShoppingCart.Generics
{
    public static class IncludedEntities
    {
        public static IReadOnlyList<TypeInfo> Types;

        static IncludedEntities()
        {
            var assembly = typeof(TEntityType).GetTypeInfo().Assembly;
            var attributes = assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(ApiEntityAttribute), true).Length > 0).Select(t => t.GetTypeInfo()).ToList();
            Types = attributes;
        }
    }
}
