namespace Cart.Api.Response
{
    using Newtonsoft.Json;

    public class ErrorResponse
    {
        public int StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }

        public ErrorResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = string.IsNullOrEmpty(message) ? GetDefaultMessageForStatusCode(statusCode) : message;
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    return "Bad Request";
                case 401:
                    return "You are Unauthorized";
                case 403:
                    return "Forbidden";
                case 404:
                    return "Resource not found";
                case 415:
                    return "Unsopported media type";
                case 500:
                    return "An unknown error has occurred, please try later";
                default:
                    return "An unknown error has occurred";
            }
        }
    }
}
