namespace api.Modules.CategoryModule.DTOs.Responses
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int SortOrder { get; set; }
        public Guid? ParentId { get; set; }
    }
}
