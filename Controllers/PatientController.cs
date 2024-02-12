using AmazeCare.Interfaces;
using AmazeCare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AmazeCare.Models.DTOs;

namespace AmazeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        IPatientAdminService _adminService;
        IPatientUserService _userService;
        public PatientController(IPatientAdminService adminService, IPatientUserService
       userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        /// <summary>
        /// Gets the list of all patients.
        /// </summary>
        /// <returns>A list of patients.</returns>
        /// 
        [Authorize(Roles = "Admin")]
        [Route("/View All Patients")]
        [HttpGet]

        public async Task<List<Patients>> GetPatient()
        {
            var patient = await _adminService.GetPatientList();
            return patient;
        }

        /// <summary>
        /// Gets the patient details by ID.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>The patient details.</returns>

        [Authorize]
        [Route("/View Patient By Id")]
        [HttpGet]
        public async Task<Patients> GetPatientById(int id)
        {
            var patient = await _userService.GetPatient(id);
            return patient;
        }

        /// <summary>
        /// Adds a new patient.
        /// </summary>
        /// <param name="patient">The patient details.</param>
        /// <returns>The added patient details.</returns>

<<<<<<< HEAD
        [Authorize(Roles = "Admin")]
        [Route("Add Patient")]
        [HttpPost]
        public async Task<Patients> PostPatient(Patients patient)
        {
            patient = await _adminService.AddPatient(patient);
            return patient;
        }
=======
        //[Authorize(Roles = "Admin")]
        //[Route("Add Patient")]

        //[HttpPost]
        //public async Task<Patients> PostPatient(Patients patient)
        //{
        //    patient = await _adminService.AddPatient(patient);
        //    return patient;
        //}
>>>>>>> dd4b272acb48671b91d7ab6cd129ae408cb07a26

        /// <summary>
        /// Updates the age of the patient.
        /// </summary>
        /// <param name="patientDTO">The patient DTO containing the ID and new age.</param>
        /// <returns>The updated patient details.</returns>
        /// 

        [Authorize(Roles = "Patient,Admin")]
        [Route("/Update Patient Age")]
        [HttpPut]
        public async Task<Patients> UpdatePatientAge(PatientAgeDTO patientDTO)
        {
            var patient = await _adminService.UpdatePatientAge(patientDTO.Id, patientDTO.Age);
            return patient;
        }

        /// <summary>
        /// Updates the mobile number of the patient.
        /// </summary>
        /// <param name="patientDTO">The patient DTO containing the ID and new mobile number.</param>
        /// <returns>The updated patient details.</returns>

        [Authorize(Roles = "Patient,Admin")]
        [Route("Update Mobile Number")]
        [HttpPut]
        public async Task<Patients> UpdatePatientMobile(PatientMobileDTO patientDTO)
        {
            var patient = await _adminService.UpdatePatientMobile(patientDTO.Id,
           patientDTO.Mobile);
            return patient;
        }

        /// <summary>
        /// Delete the patient from database.
        /// </summary>
        /// <param name="id">The ID of the patient to delete.</param>
        /// <returns>The deleted patient details.</returns>

        [Authorize(Roles = "Patient,Admin")]
        [Route("Delete Patient")]
        [HttpDelete]
        public async Task<Patients> DeletePatient(int id)
        {
            var patient = await _adminService.DeletePatient(id);
            return patient;
        }

        /// <summary>
        /// Updates the whole patient details.
        /// </summary>
        /// <param name="patients">The updated patient details.</param>
        /// <returns>The updated patient details.</returns>
        /// 

        [Authorize(Roles = "Patient,Admin")]
        [Route("/Update whole Of The Patient")]

        [HttpPut]
        public async Task<Patients> UpdatePatient(Patients patients)
        {
            var doctor = await _adminService.UpdatePatient(patients);
            return doctor;
        }
    }
}
