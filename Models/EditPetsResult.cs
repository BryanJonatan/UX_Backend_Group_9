using System.Net;

namespace PetPals_BackEnd_Group_9.Models
{
    public record EditPetsResult(HttpStatusCode Status, string Title, string Detail)
    {
        public static EditPetsResult Success() => new(HttpStatusCode.OK, "Success", "Pet updated successfully");
        public static EditPetsResult Failure(HttpStatusCode status, string title, string detail) => new(status, title, detail);
    }
}
