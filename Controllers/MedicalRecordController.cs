using AmazeCare.Interfaces;
using AmazeCare.Models;
using AmazeCare.Models.DTOs;
using AmazeCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {

            _medicalRecordService = medicalRecordService;
        }

        /// <summary>
        /// Gets all medical records
        /// </summary>
        /// <returns>List of medical records</returns>
        /// 
        [Authorize(Roles = "Admin")]
        [Route("/View All MedicalRecords")]
        [HttpGet]
        public async Task<List<MedicalRecords>> GetMedicalRecord()
        {
            var medicalRecords = await _medicalRecordService.GetMedicalRecordList();
            return medicalRecords;
        }


        /// <summary>
        /// Gets medical record by id
        /// </summary>
        /// <param name="id">Id of the medical record</param>
        /// <returns>Medical record object</returns>
        /// 
        [Authorize]
        [Route("/View MedicalRecord By Id")]
        [HttpGet]
        public async Task<MedicalRecords> GetMedicalRecordById(int id)
        {
            var medicalRecord = await _medicalRecordService.GetMedicalRecordById(id);
            return medicalRecord;
        }

        /// <summary>
        /// Adds a medical record
        /// </summary>
        /// <param name="medicalRecords">Medical record object</param>
        /// <returns>Added medical record object</returns>

        [Authorize(Roles = "Doctor")]
        [Route("Add MedicalRecord")]
        [HttpPost]
        public async Task<MedicalRecords> PostMedicalRecord(MedicalRecords medicalRecords)
        {
            medicalRecords = await _medicalRecordService.AddMedicalRecord(medicalRecords);
            return medicalRecords;
        }


        /// <summary>
        /// Gets medical record by appointment id
        /// </summary>
        /// <param name="Id">Id of the appointment</param>
        /// <returns>List of medical records for the appointment</returns>

        [Authorize]
        [Route("/View MedicalRecord By AppointmentId")]
        [HttpGet]
        public async Task<List<PatientViewMedicalRecordDTO>> GetMedicalRecordByAppointment(int Id)
        {

            var medicalRecordDetailsList = await _medicalRecordService.GetMedicalRecordByAppointment(Id);
            return medicalRecordDetailsList;


        }

        /// <summary>
        /// Gets all medical records by patient id
        /// </summary>
        /// <param name="Id">Id of the patient</param>
        /// <returns>List of medical records for the patient</returns>
        /// 

        [Authorize(Roles = "Admin,Patient")]
        [Route("/View All MedicalRecords By PatientId")]
        [HttpGet]
        public async Task<List<PatientViewMedicalRecordDTO>> GetMedicalRecordByPatientId(int Id)
        {

            var medicalRecordDetailsList = await _medicalRecordService.GetMedicalRecordByPatientId(Id);
            return medicalRecordDetailsList;


        }
    }
}
