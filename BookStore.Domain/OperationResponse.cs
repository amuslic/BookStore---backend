namespace BookStore.Domain;

public interface IOperationResponse
{
    public interface IOperationResponse<out TResponse>
          where TResponse : class
    {
        /// <summary>
        /// Operation result
        /// </summary>
        OperationResult OperationResult { get; }

        /// <summary>
        /// Entity reference to be set for successful operations
        /// </summary>
        TResponse? Response { get; }

        bool Success { get; }
    }

    public static class OperationResponse
    {
        /// <summary>
        /// Creates an IOperationResponse with a valid Response and OperationResult.Succeeded
        /// </summary>
        /// <typeparam name="TResponse">Type of Response</typeparam>
        /// <param name="response">Valid Response</param>
        /// <returns></returns>
        public static IOperationResponse<TResponse> Success<TResponse>(TResponse response)
            where TResponse : class => new InternalOperationResponse<TResponse>(OperationResult.Succeeded, response, true);

        /// <summary>
        /// Creates an IOperationResponse with an invalid Response and the given OperationResult
        /// </summary>
        /// <typeparam name="TResponse">Type of Response</typeparam>
        /// <param name="result">Invalid Response</param>
        /// <returns>IOperationResponse with invalid Response and the error OperationResult</returns>
        public static IOperationResponse<TResponse> Error<TResponse>(OperationResult result)
            where TResponse : class => new InternalOperationResponse<TResponse>(result, null, false);

        private class InternalOperationResponse<TResponse> : IOperationResponse<TResponse>
            where TResponse : class
        {
            public InternalOperationResponse(OperationResult result, TResponse? response, bool success)
            {
                OperationResult = result;
                Response = response;
                Success = success;
            }

            public OperationResult OperationResult { get; }
            public TResponse? Response { get; }
            public bool Success { get; }
        }
    }
}
