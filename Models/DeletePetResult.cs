using System.Net;

namespace PetPals_BackEnd_Group_9.Models
{
    public record DeletePetResult(HttpStatusCode Status, string Title, string Detail)
    {
        public static DeletePetResult Success() => new(HttpStatusCode.OK, "Success", "Pet has been removed");
        public static DeletePetResult Failure(HttpStatusCode Status, string title, string detail) => new(Status, title, detail);
    }
}
