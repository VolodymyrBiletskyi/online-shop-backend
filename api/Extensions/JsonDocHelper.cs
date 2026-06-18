using System.Text.Json;

namespace api.Extensions
{
    public static class JsonDocHelper
    {
        public static JsonDocument ParseOrEmpty(string? json)
        {
            if (string.IsNullOrWhiteSpace(json)) return JsonDocument.Parse("{}");
            return JsonDocument.Parse("{}");
        }
    }
}