using System.Runtime.Serialization;

namespace AmazeCare.Exceptions
{
    [Serializable]
    public class NoSuchDoctorException : Exception
    {
        

        string message;
        public NoSuchDoctorException()
        {
            message = "NO Doctor with the given id";
        }
        public override string Message => message;


    }
}