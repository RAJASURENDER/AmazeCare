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


        /// <summary>
        /// Returns a list of all doctors.
        /// </summary>
        /// <returns>A list of doctors.</returns>

        [Authorize]
        [Route("/View All Doctors")]
        [HttpGet]
        public async Task<List<Doctors>> GetDoctor()
        {
            var doctor = await _adminService.GetDoctorList();
            return doctor;
        }

        /// <summary>
        /// Returns a doctor by ID.
        /// </summary>
        /// <param name="id">The ID of the doctor to retrieve.</param>
        /// <returns>The doctor with the specified ID.</returns>

        [Authorize(Roles ="Admin")]
        [Route("/View Doctor By Id")]
        [HttpGet]
        public async Task<Doctors> GetDoctorById(int id)
        {
            var doctor = await _userService.GetDoctor(id);
            return doctor;
        }



        /// <summary>
        /// Adds a doctor to the system.
        /// </summary>
        /// <param name="doctor">The doctor to add.</param>
        /// <returns>The added doctor.</returns>
        /// 
        [Route("Add Doctor")]
        [HttpPost]
        public async Task<Doctors> PostDoctor(Doctors doctor)
        {
            doctor = await _adminService.AddDoctor(doctor);
            return doctor;
        }



        /// <summary>
        /// Updates a doctor's experience.
        /// </summary>
        /// <param name="doctorDTO">The doctor's experience to update.</param>
        /// <returns>The updated doctor.</returns>
        /// 
        [Authorize(Roles = "Admin")]
        [Route("/Update Experience Of The Doctor")]
        [HttpPut]
        public async Task<Doctors> UpdateDoctorExperience(DoctorExperienceDTO doctorDTO)
        {
            var doctor = await _adminService.UpdateDoctorExperience(doctorDTO.Id,doctorDTO.Experience);
            return doctor;
        }

        /// <summary>
        /// Updates a doctor's qualification.
        /// </summary>
        /// <param name="doctorDTO">The doctor's qualification to update.</param>
        /// <returns>The updated doctor.</returns>

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
        /// <summary>
        /// Updates a doctor's information.
        /// </summary>
        /// <param name="doctors">The updated doctor information.</param>
        /// <returns>The updated doctor.</returns>
        /// 

        [Authorize(Roles = "Admin")]
        [Route("/Update whole Of The Doctor")]

        [HttpPut]
        public async Task<Doctors> UpdateDoctor(Doctors
     doctors)
        {
            var doctor = await _adminService.UpdateDoctor(doctors);
            return doctor;
        }


        /// <summary>
        /// Delete a doctor from the Application.
        /// </summary>
        /// <param name="id">The ID of the doctor to delete.</param>
        /// <returns
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
