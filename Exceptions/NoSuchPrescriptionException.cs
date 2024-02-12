using System.Runtime.Serialization;

namespace AmazeCare.Exceptions
{
    [Serializable]
    public class NoSuchPrescriptionException : Exception
    {


        string message;
        public NoSuchPrescriptionException()
        {
            message = "NO Prescription with the given id";
        }
        public override string Message => message;


    }

}