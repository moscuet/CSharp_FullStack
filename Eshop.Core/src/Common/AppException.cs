using System.Net;

namespace Eshop.Core.src.Common
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public AppException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
            ErrorMessage = message;
        }


        public static AppException NotFound(string message = "Resource Not Found")
        {
            return new AppException(HttpStatusCode.NotFound, message);
        }

        // Predefined exceptions
        public static AppException BadRequest(string message = "Bad Request")
        {
            return new AppException(HttpStatusCode.BadRequest, message);
        }

        public static AppException Unauthorized(string message = "Unauthorized Access")
        {
            return new AppException(HttpStatusCode.Unauthorized, message);
        }

        public static AppException Forbidden(string message = "Forbidden Access")
        {
            return new AppException(HttpStatusCode.Forbidden, message);
        }

        public static AppException Conflict(string message = "Conflict")
        {
            return new AppException(HttpStatusCode.Conflict, message);
        }

        public static AppException UnprocessableEntity(string message = "Unprocessable Entity")
        {
            return new AppException(HttpStatusCode.UnprocessableEntity, message);
        }

        public static AppException InternalServerError(string message = "Internal Server Error")
        {
            return new AppException(HttpStatusCode.InternalServerError, message);
        }

        // Custom Error with specific status code
        public static AppException CustomError(HttpStatusCode statusCode, string message)
        {
            return new AppException(statusCode, message);
        }
    }
}
