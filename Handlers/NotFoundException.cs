
using System.Net;

namespace PetPals_BackEnd_Group_9.Handlers
{
    [Serializable]
    internal class NotFoundException : Exception
    {

        public NotFoundException()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}