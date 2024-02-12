using System.Runtime.Serialization;

namespace AmazeCare.Repositories
{
    [Serializable]
    public class NoSuchMedicalRecordException : Exception
    {


        string message;
        public NoSuchMedicalRecordException()
        {
            message = "NO MedicalRecord with the given id";
        }
        public override string Message => message;


    }
}