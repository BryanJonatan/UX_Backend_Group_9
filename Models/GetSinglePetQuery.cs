using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetSinglePetQuery : IRequest<GetSinglePetResponse>
    {
        public string Slug { get; set; }
        public GetSinglePetQuery(string slug)
        {
            Slug = slug;
        }
    }
}
