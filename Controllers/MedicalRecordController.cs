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

        [Authorize(Roles = "Admin")]
        [Route("/View All MedicalRecords")]
        [HttpGet]
        public async Task<List<MedicalRecords>> GetMedicalRecord()
        {
            var medicalRecords = await _medicalRecordService.GetMedicalRecordList();
            return medicalRecords;
        }

        [Authorize]
        [Route("/View MedicalRecord By Id")]
        [HttpGet]
        public async Task<MedicalRecords> GetMedicalRecordById(int id)
        {
            var medicalRecord = await _medicalRecordService.GetMedicalRecordById(id);
            return medicalRecord;
        }

        [Authorize(Roles = "Doctor")]
        [Route("Add MedicalRecord")]
        [HttpPost]
        public async Task<MedicalRecords> PostMedicalRecord(MedicalRecords medicalRecords)
        {
            medicalRecords = await _medicalRecordService.AddMedicalRecord(medicalRecords);
            return medicalRecords;
        }




        [Authorize]
        [Route("/View MedicalRecord By AppointmentId")]
        [HttpGet]
        public async Task<List<PatientViewMedicalRecordDTO>> GetMedicalRecordByAppointment(int Id)
        {

            var medicalRecordDetailsList = await _medicalRecordService.GetMedicalRecordByAppointment(Id);
            return medicalRecordDetailsList;


        }


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
