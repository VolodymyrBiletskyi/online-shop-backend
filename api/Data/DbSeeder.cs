using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using api.Data;
using api.Models;

namespace api.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // уже что-то есть — выходим
            if (context.Products.Any())
                return;

            var random = new Random();

            // --- Категории (создаём, если нет) ---

            var existingSlugs = new HashSet<string>(
                context.Products
                    .Select(p => p.Slug)
                    .Where(s => s != null),
                StringComparer.OrdinalIgnoreCase
            );

            var newSlugs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var existingCategories = context.Categories.ToList();
            Category GetOrCreateCategory(string name, string slug, int sortOrder)
            {
                var category = existingCategories
                    .FirstOrDefault(c => c.Slug == slug || c.Name == name);

                if (category != null)
                {
                    return category;
                }

                category = new Category
                {
                    Name = name,
                    Slug = slug,
                    SortOrder = sortOrder
                };

                context.Categories.Add(category);
                existingCategories.Add(category);
                return category;
            }

            var electronics = GetOrCreateCategory("Electronics", "electronics", 1);
            var clothing    = GetOrCreateCategory("Clothing", "clothing", 2);
            var footwear    = GetOrCreateCategory("Footwear", "footwear", 3);
            var pets        = GetOrCreateCategory("Pet Supplies", "pet-supplies", 4);

            context.SaveChanges();

            // --- Словари для генерации ---

            var electronicsBrands    = new[] { "Apple", "Samsung", "Xiaomi", "Sony", "LG", "Huawei" };
            var electronicsBaseNames = new[] { "Smartphone", "TV", "Laptop", "Tablet", "Headphones", "Smartwatch" };
            var electronicsSpecs     = new[] { "Pro", "Ultra", "Mini", "Max", "4K", "64GB", "128GB", "256GB" };

            var clothingBrands    = new[] { "Nike", "Adidas", "Puma", "Reebok", "H&M", "Zara" };
            var clothingBaseNames = new[] { "T-Shirt", "Hoodie", "Jacket", "Jeans", "Shorts" };
            var clothingColors    = new[] { "Black", "White", "Blue", "Red", "Green", "Grey" };
            var clothingSizes     = new[] { "XS", "S", "M", "L", "XL" };

            var footwearBrands    = new[] { "Nike", "Adidas", "New Balance", "Vans", "Converse" };
            var footwearBaseNames = new[] { "Sneakers", "Running Shoes", "Boots", "Sandals" };
            var footwearSizes     = new[] { 37, 38, 39, 40, 41, 42, 43, 44 };

            var petBrands    = new[] { "Royal Canin", "Whiskas", "Pedigree", "Purina" };
            var petBaseNames = new[] { "Cat Food", "Dog Food", "Snacks" };
            var petFlavours  = new[] { "Chicken", "Beef", "Salmon", "Turkey" };

            var products = new List<Product>();

            // --- хелпер: создать товар + варианты ---
            void AddProductWithVariants(
                Category category,
                string productName,
                string? description,
                decimal minPrice,
                decimal maxPrice,
                Action<ProductVariant>? customizeVariant = null)
            {
                var basePrice = Math.Round(
                    minPrice + (decimal)random.NextDouble() * (maxPrice - minPrice),
                    2);

                var slugBase = Slugify(productName);
                var uniqueSlug = GenerateUniqueSlug(slugBase, existingSlugs,newSlugs);

                var product = new Product
                {
                    CategoryId = category.Id,
                    Category   = category,
                    Name       = productName,
                    Slug       = uniqueSlug,
                    Description = description,
                    SortOrder   = 0,
                    BasePrice   = basePrice,
                    IsActive    = true,
                    CreatedAt   = DateTime.UtcNow
                };

                var variantsCount = random.Next(1, 5); // 1–4 варианта

                for (int i = 0; i < variantsCount; i++)
                {
                    var variant = new ProductVariant
                    {
                        Product       = product,
                        ProductId     = product.Id,
                        Sku           = GenerateSku(productName, i, random),
                        Title         = variantsCount == 1 ? "Default" : $"Option {i + 1}",
                        InitAvaliable = random.Next(0, 250)
                    };

                    // иногда переопределяем цену
                    if (random.NextDouble() < 0.6)
                    {
                        var factor = 0.9 + random.NextDouble() * 0.4; // 0.9–1.3
                        variant.PriceOverride = Math.Round(basePrice * (decimal)factor, 2);
                    }

                    // иногда задаём вес
                    if (random.NextDouble() < 0.5)
                    {
                        variant.Weight = Math.Round((decimal)(0.1 + random.NextDouble() * 4.9), 2);
                    }

                    customizeVariant?.Invoke(variant);

                    product.Variants.Add(variant);
                }

                products.Add(product);
            }

            // --- Electronics ---
            for (int i = 0; i < 150; i++)
            {
                var brand    = electronicsBrands[random.Next(electronicsBrands.Length)];
                var baseName = electronicsBaseNames[random.Next(electronicsBaseNames.Length)];
                var spec     = electronicsSpecs[random.Next(electronicsSpecs.Length)];

                var title       = $"{brand} {baseName} {spec}";
                var description = $"{brand} {baseName} with {spec} configuration.";

                AddProductWithVariants(
                    electronics,
                    title,
                    description,
                    200m,
                    2500m,
                    variant =>
                    {
                        variant.Attributes["Brand"]    = brand;
                        variant.Attributes["Spec"]     = spec;
                        variant.Attributes["Type"]     = baseName;
                        variant.Attributes["Warranty"] = random.Next(12, 37); // месяцев
                    });
            }

            // --- Clothing ---
            for (int i = 0; i < 150; i++)
            {
                var brand    = clothingBrands[random.Next(clothingBrands.Length)];
                var baseName = clothingBaseNames[random.Next(clothingBaseNames.Length)];
                var color    = clothingColors[random.Next(clothingColors.Length)];

                var title       = $"{brand} {color} {baseName}";
                var description = $"{color} {baseName} by {brand}.";

                AddProductWithVariants(
                    clothing,
                    title,
                    description,
                    15m,
                    150m,
                    variant =>
                    {
                        var size = clothingSizes[random.Next(clothingSizes.Length)];
                        variant.Attributes["Brand"]  = brand;
                        variant.Attributes["Color"]  = color;
                        variant.Attributes["Type"]   = baseName;
                        variant.Attributes["Size"]   = size;
                        variant.Attributes["Gender"] =
                            random.NextDouble() < 0.33 ? "Men" :
                            random.NextDouble() < 0.66 ? "Women" : "Unisex";
                    });
            }

            // --- Footwear ---
            for (int i = 0; i < 150; i++)
            {
                var brand    = footwearBrands[random.Next(footwearBrands.Length)];
                var baseName = footwearBaseNames[random.Next(footwearBaseNames.Length)];

                var title       = $"{brand} {baseName}";
                var description = $"{baseName} by {brand}, comfortable and durable.";

                AddProductWithVariants(
                    footwear,
                    title,
                    description,
                    30m,
                    250m,
                    variant =>
                    {
                        var size = footwearSizes[random.Next(footwearSizes.Length)];
                        variant.Attributes["Brand"]  = brand;
                        variant.Attributes["Type"]   = baseName;
                        variant.Attributes["Size"]   = size;
                        variant.Attributes["Season"] =
                            random.NextDouble() < 0.5 ? "All-season" : "Summer";
                    });
            }

            // --- Pet Supplies ---
            for (int i = 0; i < 150; i++)
            {
                var brand    = petBrands[random.Next(petBrands.Length)];
                var baseName = petBaseNames[random.Next(petBaseNames.Length)];
                var flavour  = petFlavours[random.Next(petFlavours.Length)];
                var weightKg = random.Next(1, 6); // 1–5 кг

                var title       = $"{brand} {baseName} {flavour} {weightKg}kg";
                var description = $"{baseName} for pets with {flavour} flavour ({weightKg}kg).";

                AddProductWithVariants(
                    pets,
                    title,
                    description,
                    5m,
                    60m,
                    variant =>
                    {
                        variant.Attributes["Brand"]    = brand;
                        variant.Attributes["Type"]     = baseName;
                        variant.Attributes["Flavour"]  = flavour;
                        variant.Attributes["WeightKg"] = weightKg;
                        variant.Attributes["Animal"] =
                            baseName.Contains("Cat", StringComparison.OrdinalIgnoreCase) ? "Cat" :
                            baseName.Contains("Dog", StringComparison.OrdinalIgnoreCase) ? "Dog" :
                            "Universal";
                    });
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static string Slugify(string input)
        {
            var lower = input.Trim().ToLowerInvariant();
            lower = Regex.Replace(lower, @"\s+", "-");
            lower = Regex.Replace(lower, @"[^a-z0-9\-]", "");
            return lower;
        }

        private static string GenerateSku(string productName, int index, Random random)
        {
            var prefix = new string(
                productName
                    .Where(char.IsLetterOrDigit)
                    .Take(3)
                    .Select(char.ToUpperInvariant)
                    .ToArray());

            var number = random.Next(10000, 99999);
            return $"{prefix}-{index + 1}-{number}";
        }

        private static string GenerateUniqueSlug(
            string baseSlug,
            HashSet<string> existingSlugs,
            HashSet<string> newSlugs)
        {
            var candidate = baseSlug;
            var counter = 1;

            while(existingSlugs.Contains(candidate) || newSlugs.Contains(candidate))
            {
                candidate = $"{baseSlug}-{counter}";
                counter ++;
            }

            newSlugs.Add(candidate);
            return candidate;
        }
        
    }
}
