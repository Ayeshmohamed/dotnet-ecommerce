namespace Apps.Dtos.Categories
{
    public class FilterCategories
    {
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? ParentId { get; set; }

        public bool? IsActive { get; set;}

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
