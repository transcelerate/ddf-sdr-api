using System.Linq;

namespace TransCelerate.SDR.Core.Utilities.Helpers
{
    public static class StringOperations
    {
        public static string ChangeToCamelCase(this string value)
        {
            return string.Join(".", value?.Split(".").Select(key => $"{key?[..1]?.ToLower()}{key?[1..]}"));
        }

        public static string RemoveEntity(this string value)
        {
            return value.Replace("Entity","");
        }
        public static string RemoveDto(this string value)
        {
            return value.Replace("Dto", "");
        }
    }
}
