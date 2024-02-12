using AmazeCare.Models;
using AmazeCare.Models.DTOs;

namespace AmazeCare.Mappers
{
    public class RegisterToPatient
    {
        Patients patient;
        public RegisterToPatient(RegisterPatientDTO register)
        {
            patient = new Patients();
            patient.PatientName = register.PatientName;
            patient.Age = register.Age;
            patient.Gender = register.Gender;
            patient.DateOfBirth = register.DateOfBirth;
            patient.ContactNumber = register.ContactNumber;
            patient.Username = register.Username;
        }
        public Patients GetPatient()
        {
            return patient;
        }
    }
}
