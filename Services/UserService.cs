using AmazeCare.Exceptions;
using AmazeCare.Interfaces;
using AmazeCare.Mappers;
using AmazeCare.Models;
using AmazeCare.Models.DTOs;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace AmazeCare.Services
{

    public class UserService : IUserService
    {
        private readonly IRepository<int, Patients> _patientRepository;
        private readonly IRepository<int, Doctors> _doctorRepository;
        private readonly IRepository<string, User> _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly ITokenService _tokenService;
        private readonly User _adminUser; 


        public UserService(IRepository<int, Patients> patientRepository,
                           IRepository<int, Doctors> doctorRepository,
                           IRepository<string, User> userRepository,
                           ITokenService tokenService,
                           ILogger<UserService> logger)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _logger = logger;

            _adminUser = new User
            {
                Username = "Admin",
                Role = "Admin",
                Password = GetPasswordEncrypted("admin", GenerateRandomKey()), 
                Key = GenerateRandomKey()
            };

        }

        public async Task<LoginUserDTO> Login(LoginUserDTO user)
        {
            try
            {
                if (user.Username == "Admin" && user.Password == "admin")
                {
                    user.Role = "Admin";
                    user.Password = "";
                    user.Token = await _tokenService.GenerateToken(user);
                    return user;
                }
                else
                {
                    var myUser = await _userRepository.GetAsync(user.Username);
                    if (myUser == null)
                    {
                        throw new InvalidUserException();
                    }

                    var userPassword = GetPasswordEncrypted(user.Password, myUser.Key);
                    var checkPasswordMatch = ComparePasswords(myUser.Password, userPassword);
                    if (checkPasswordMatch)
                    {
                        user.Password = ""; 
                        user.Role = myUser.Role;
                        user.Token = await _tokenService.GenerateToken(user);
                        return user;
                    }
                    else
                    {
                        throw new InvalidUserException();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging in user: {Username}", user.Username);
                throw;
            }
        }
        private bool ComparePasswords(byte[] password, byte[] userPassword)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] != userPassword[i])
                    return false;
            }
            return true;
        }

        private byte[] GetPasswordEncrypted(string password, byte[] key)
        {
            HMACSHA256 hmac = new HMACSHA256(key);
            var userpassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return userpassword;
        }

        public async Task<LoginUserDTO> RegisterPatient(RegisterPatientDTO user)
        {
            try
            {
                User myUser = new RegisterToUser(user).GetUser();
                myUser = await _userRepository.Add(myUser);
                Patients patient = new RegisterToPatient(user).GetPatient();
                patient = await _patientRepository.Add(patient);
                LoginUserDTO result = new LoginUserDTO
                {
                    Username = myUser.Username,
                    Role = myUser.Role,
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user: {Username}", user.Username);
                throw;
            }
        }

        public async Task<LoginUserDTO> RegisterDoctor(RegisterDoctorDTO user)
        {
            try
            {
                User myUser = new RegisterToUser(user).GetUser();
                myUser = await _userRepository.Add(myUser);
                Doctors doctor = new RegisterToDoctor(user).GetDoctor();
                doctor = await _doctorRepository.Add(doctor);
                LoginUserDTO result = new LoginUserDTO
                {
                    Username = myUser.Username,
                    Role = myUser.Role,
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user: {Username}", user.Username);
                throw;
            }
        }

        private static byte[] GenerateRandomKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[64]; 
                rng.GetBytes(key);
                return key;
            }
        }

        
    }
}
