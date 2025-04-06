using System.Net;

namespace PetPals_BackEnd_Group_9.Models
{
    public record DeleteServiceResult(HttpStatusCode Status, string Title, string Detail)
    {
        public static DeleteServiceResult Success() => new(HttpStatusCode.OK, "Success", "Service has been removed");
        public static DeleteServiceResult Failure(HttpStatusCode Status, string title, string detail) => new(Status, title, detail);
    }
}
