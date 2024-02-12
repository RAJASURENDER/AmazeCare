using System.Runtime.Serialization;

namespace AmazeCare.Exceptions
{
    [Serializable]
    internal class NoSuchPatientException : Exception
    {

        string message;
        public NoSuchPatientException()
        {
            message = "NO Patient with the given id";
        }
        public override string Message => message;

    }
}