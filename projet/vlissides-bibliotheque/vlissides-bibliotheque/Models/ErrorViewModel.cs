namespace vlissides_bibliotheque.Models
{
    public class ErrorViewModel
    {
        public string? RequestErrorViewModelId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
