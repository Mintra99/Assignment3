namespace Assignment3.Helpers
{
    public class NotFoundResponse
    {
        public string Type { get; set; } = "Error";
        public string Title { get; set; } = "Not found";
        public int Status { get; set; } = 404;
        public string Detail { get; set; }

        public NotFoundResponse(string detail)
        {
            Detail = detail;
        }
    }
}
