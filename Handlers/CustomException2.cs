namespace PetPals_BackEnd_Group_9.Handlers
{
    public class CustomException2 : Exception
    {
        public string ErrorCode { get; }
        public int StatusCode { get; }

        public CustomException2(string message, string errorCode, int statusCode) : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }
}
