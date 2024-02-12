using AmazeCare.Models.DTOs;

namespace AmazeCare.Interfaces
{
    public interface IUserService
    {
        public Task<LoginUserDTO> Login(LoginUserDTO user);
        public Task<LoginUserDTO> RegisterPatient(RegisterPatientDTO user);
        public Task<LoginUserDTO> RegisterDoctor(RegisterDoctorDTO user);
    }
}