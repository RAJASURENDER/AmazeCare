using AmazeCare.Interfaces;
using AmazeCare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AmazeCare.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace AmazeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorAdminService _adminService;
        private readonly IDoctorUserService _userService;
        public DoctorsController(IDoctorAdminService adminService, IDoctorUserService
       userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        [Authorize]
        [Route("/View All Doctors")]
        [HttpGet]
        public async Task<List<Doctors>> GetDoctor()
        {
            var doctor = await _adminService.GetDoctorList();
            return doctor;
        }

        [Authorize(Roles ="Admin")]
        [Route("/View Doctor By Id")]
        [HttpGet]
        public async Task<Doctors> GetDoctorById(int id)
        {
            var doctor = await _userService.GetDoctor(id);
            return doctor;
        }

        [Route("Add Doctor")]
        [HttpPost]
        public async Task<Doctors> PostDoctor(Doctors doctor)
        {
            doctor = await _adminService.AddDoctor(doctor);
            return doctor;
        }

        [Authorize(Roles = "Admin")]
        [Route("/Update Experience Of The Doctor")]
        [HttpPut]
        public async Task<Doctors> UpdateDoctorExperience(DoctorExperienceDTO doctorDTO)
        {
            var doctor = await _adminService.UpdateDoctorExperience(doctorDTO.Id,doctorDTO.Experience);
            return doctor;
        }

        [Authorize(Roles = "Admin")]
        [Route("/Update Qualification Of The Doctor")]
        [HttpPut]
        public async Task<Doctors> UpdateDoctorQualification(DoctorQualificationDTO
       doctorDTO)
        {
            var doctor = await _adminService.UpdateDoctorQualification(doctorDTO.Id,
           doctorDTO.Qualification);
            return doctor;
        }

        [Authorize(Roles = "Admin")]
        [Route("/Update whole Of The Doctor")]

        [HttpPut]
        public async Task<Doctors> UpdateDoctor(Doctors
     doctors)
        {
            var doctor = await _adminService.UpdateDoctor(doctors);
            return doctor;
        }

        [Authorize(Roles = "Admin")]
        [Route("/Delete Doctor")]
        [HttpDelete]
        public async Task<Doctors> DeleteDoctor(int id)
        {
            var doctor = await _adminService.DeleteDoctor(id);
            return doctor;
        }


     

    }
}
