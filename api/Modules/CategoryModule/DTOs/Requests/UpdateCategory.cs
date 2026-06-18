namespace api.Modules.CategoryModule.DTOs.Requests
{
    public class UpdateCategory
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = string.Empty;
        public int SortOrder { get; set; } = 0;
        public Guid? ParentId { get; set; } = null;
    }
}
