using MediatR;

namespace PetPals_BackEnd_Group_9.Models
{
    public class GetSingleServiceQuery : IRequest<GetSingleServiceResponse>
    {
        public string Slug { get; set; }
        public GetSingleServiceQuery(string slug)
        {
            Slug = slug;
        }
    }
}
