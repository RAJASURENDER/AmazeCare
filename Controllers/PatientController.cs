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

        [Authorize(Roles = "Admin")]
        [Route("/View All Patients")]
        [HttpGet]

        public async Task<List<Patients>> GetPatient()
        {
            var patient = await _adminService.GetPatientList();
            return patient;
        }


      
        [Authorize]
        [Route("/View Patient By Id")]
        [HttpGet]
        public async Task<Patients> GetPatientById(int id)
        {
            var patient = await _userService.GetPatient(id);
            return patient;
        }


        //[Authorize(Roles = "Admin")]
        //[Route("Add Patient")]

        //[HttpPost]
        //public async Task<Patients> PostPatient(Patients patient)
        //{
        //    patient = await _adminService.AddPatient(patient);
        //    return patient;
        //}

        [Authorize(Roles = "Patient,Admin")]
        [Route("/Update Patient Age")]
        [HttpPut]
        public async Task<Patients> UpdatePatientAge(PatientAgeDTO patientDTO)
        {
            var patient = await _adminService.UpdatePatientAge(patientDTO.Id, patientDTO.Age);
            return patient;
        }

        [Authorize(Roles = "Patient,Admin")]
        [Route("Update Mobile Number")]
        [HttpPut]
        public async Task<Patients> UpdatePatientMobile(PatientMobileDTO patientDTO)
        {
            var patient = await _adminService.UpdatePatientMobile(patientDTO.Id,
           patientDTO.Mobile);
            return patient;
        }

        [Authorize(Roles = "Patient,Admin")]
        [Route("Delete Patient")]
        [HttpDelete]
        public async Task<Patients> DeletePatient(int id)
        {
            var patient = await _adminService.DeletePatient(id);
            return patient;
        }


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
