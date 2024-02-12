using AmazeCare.Exceptions;
using AmazeCare.Interfaces;
using AmazeCare.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [Route("/Register Patient")]
        [HttpPost]
        public async Task<LoginUserDTO> RegisterPatient(RegisterPatientDTO user)
        {
            var result = await _userService.RegisterPatient(user);
            return result;
        }

        [Route("/Register Doctor")]
        [HttpPost]
        public async Task<LoginUserDTO> RegisterDoctor(RegisterDoctorDTO user)
        {
            var result = await _userService.RegisterDoctor(user);
            return result;
        }


        [Route("/Login")]
        [HttpPost]
        public async Task<ActionResult<LoginUserDTO>> Login(LoginUserDTO user)
        {
            try
            {
                var result = await _userService.Login(user);
                return Ok(result);
            }
            catch (InvalidUserException iuse)
            {
                _logger.LogCritical(iuse.Message);
                return Unauthorized("Invalid username or password");
            }

        }
    }
}
