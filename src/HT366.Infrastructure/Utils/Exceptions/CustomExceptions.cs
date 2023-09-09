namespace HT366.Infrastructure.Utils.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message)
        {
        }
    }

    public class UnAuthorizeException : Exception
    {
        public UnAuthorizeException(string message) : base(message)
        {
        }
    }
}