using System.Net;

namespace PetPals_BackEnd_Group_9.Handlers
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ApiException(HttpStatusCode statusCode, string title, string detail)
            : base(detail)
        {
            StatusCode = statusCode;
            Title = title;
            Detail = detail;
        }

        public string Title { get; }
        public string Detail { get; }

        public object ToProblemDetails(HttpContext context) => new
        {
            type = $"https://httpstatuses.com/{(int)StatusCode}",
            title = Title,
            status = (int)StatusCode,
            detail = Detail,
            instance = context.Request.Path
        };
    }
}
