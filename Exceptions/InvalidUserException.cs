using System.Runtime.Serialization;

namespace AmazeCare.Exceptions
{
    [Serializable]
    internal class InvalidUserException : Exception
    {


        string message;
        public InvalidUserException()
        {
            message = "NO User with the given id";
        }
        public override string Message => message;
    }
}