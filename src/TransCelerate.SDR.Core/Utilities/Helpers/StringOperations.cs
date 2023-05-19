using System.Linq;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class StringOperations
    {
        public static string ChangeToCamelCase(this string value)
        {
            return string.Join(".", value?.Split(".").Select(key => $"{key?[..1]?.ToLower()}{key?[1..]}"));
        }
    }
}
