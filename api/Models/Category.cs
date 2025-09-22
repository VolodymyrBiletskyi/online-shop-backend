using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid? ParentId { get; set; }
        public Category? Parent { get; set; }

        public ICollection<Category> Children { get; set; } = new List<Category>();
        
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int SortOrder { get; set; }
    }
}