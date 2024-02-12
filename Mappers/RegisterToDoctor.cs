using AmazeCare.Models;
using AmazeCare.Models.DTOs;

namespace AmazeCare.Mappers
{
    public class RegisterToDoctor
    {
        Doctors doctor;
        
        public RegisterToDoctor(RegisterDoctorDTO register)
        {
            doctor = new Doctors();
            doctor.DoctorName = register.DoctorName;
            doctor.Speciality = register.Speciality;
            doctor.Experience = register.Experience;
            doctor.Qualification = register.Qualification;
            doctor.Designation = register.Designation;
            doctor.Username = register.Username;
        }
        public Doctors GetDoctor()
        {
            return doctor;
        }
    }
}
