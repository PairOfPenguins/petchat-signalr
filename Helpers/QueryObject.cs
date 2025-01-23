namespace petchat.Helpers
{
    public class QueryObject
    {
        public string? Content {  get; set; } = null;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;

    }
}
