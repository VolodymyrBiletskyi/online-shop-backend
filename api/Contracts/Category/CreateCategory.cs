using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Contracts.Category
{
    public class CreateCategory
    {
        public string Name { get; set; } = null!;
        public string? Slug { get; set; } = string.Empty;
        public int SortOrder { get; set; } = 0;
        public Guid? ParentId { get; set; } = null;
    }
}