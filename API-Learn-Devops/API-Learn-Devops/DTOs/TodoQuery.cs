namespace API_Learn_Devops.DTOs
{
    public class TodoQuery
    {
        public string? Search { get; set; }
        public string? Status { get; set; } = "all"; // all | active | completed
        public string? SortBy { get; set; } = "createdAt"; // createdAt | title
        public string? SortOrder { get; set; } = "desc"; // asc | desc
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
