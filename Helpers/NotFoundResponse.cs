namespace Assignment3.Helpers
{
    public class NotFoundResponse
    {
        public string Type { get; set; } = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
        public string Title { get; set; } = "Not found";
        public int Status { get; set; } = 404;
        public string Detail { get; set; }

        public NotFoundResponse(string detail)
        {
            Detail = detail;
        }
    }
}
