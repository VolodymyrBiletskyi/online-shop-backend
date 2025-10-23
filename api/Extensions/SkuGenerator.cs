using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace api.Extensions
{
    public static class SkuGenerator
    {
        public static string Generate(string? titleForPrefix = "Sku")
        {
            var prefix = (titleForPrefix ?? "Sku").Trim();
            if (prefix.Length > 3) prefix = prefix[..3];
            prefix = prefix.ToUpperInvariant();
            return $"{prefix?.ToUpper()}-{DateTime.UtcNow.ToString("yyyyMMdd")}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
        }
    }
}