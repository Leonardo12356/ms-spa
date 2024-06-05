namespace ms_spa.Api.Contract
{
    public class ModelErrorContract
    {
        public int StatusCode { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
    }
}