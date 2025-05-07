
namespace Ecom.API.Errors
{
    public class BaseCommonResponse
    {
        public BaseCommonResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? DefaultMessageForStatusCode(statusCode);
        }

        private string DefaultMessageForStatusCode(int statusCode)
        => statusCode switch
        {
            400 => "Bad request",
            401 => "You are not authorized,",
            404 => "Resource not found",
            500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change",
            _ => null
        };

        public int StatusCode { get; set; }
        public string Message { get; set; }


    }
}
