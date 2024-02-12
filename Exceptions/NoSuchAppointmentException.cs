using System.Runtime.Serialization;

namespace AmazeCare.Exceptions
{
    [Serializable]
    internal class NoSuchAppointmentException : Exception
    {
        

        string message;
        public NoSuchAppointmentException()
        {
            message = "NO Appointment with the given id";
        }
        public override string Message => message;
    }
}