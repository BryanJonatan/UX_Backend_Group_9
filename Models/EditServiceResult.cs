using System.Net;

namespace PetPals_BackEnd_Group_9.Models
{
    public record EditServiceResult(HttpStatusCode Status, string Title, string Detail)
    {
        public static EditServiceResult Success() => new(HttpStatusCode.OK, "Success", "Service updated successfully");
        public static EditServiceResult Failure(HttpStatusCode status, string title, string detail) => new(status, title, detail);
    }

}
