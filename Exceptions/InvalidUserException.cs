using System.Runtime.Serialization;

namespace AmazeCare.Exceptions
{
    [Serializable]
    public class InvalidUserException : Exception
    {
<<<<<<< HEAD

=======
        /// <summary>
        /// Represents an exception thrown when an invalid user is encountered.
        /// </summary>
>>>>>>> a9cf6884d5fa6a42752241f1b0486319d56b8532

        string message;
        public InvalidUserException()
        {
            message = "NO User with the given id";
        }
        public override string Message => message;
    }
}