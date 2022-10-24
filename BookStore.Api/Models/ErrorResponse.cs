namespace BookStoreApi.Models
{
    public class ErrorResponse
    {
        public class ErrorResponseModel
        {
            public string Title { get; set; } = "";


            public string ErrorCode { get; set; } = "";


            public string TraceId { get; set; } = "";


            public IReadOnlyList<ErrorMessage> Errors { get; set; } = new ErrorMessage[0];

        }

        public class ErrorMessage
        {
            public string Content { get; set; } = "";

        }
    }
}
