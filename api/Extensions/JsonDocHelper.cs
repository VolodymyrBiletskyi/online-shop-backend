using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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