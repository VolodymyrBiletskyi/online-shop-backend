namespace api.Modules.CategoryModule.DTOs.Requests
{
    public class CreateCategory
    {
        public string Name { get; set; } = null!;
        public int SortOrder { get; set; } = 0;
        public Guid? ParentId { get; set; } = null;
    }
}
