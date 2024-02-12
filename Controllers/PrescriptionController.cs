using AmazeCare.Interfaces;
using AmazeCare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {

            _prescriptionService = prescriptionService;
        }

        [Authorize(Roles = "Admin")]
        [Route("View All Prescriptions")]
        [HttpGet]
        public async Task<List<Prescriptions>> GetPrescription()
        {
            var prescriptions = await _prescriptionService.GetPrescriptionList();
            return prescriptions;
        }


        [Authorize(Roles = "Patient")]
        [Route("/View Prescription  By RecordId")]
        [HttpGet]
        public async Task<Prescriptions> GetPrescriptionById(int id)
        {
            var prescription = await _prescriptionService.GetPrescriptionById(id);
            return prescription;
        }

        [Authorize(Roles = "Doctor")]
        [Route("Add Prescription")]
        [HttpPost]
        public async Task<Prescriptions> PostMedicalRecord(Prescriptions prescriptions)
        {
            prescriptions = await _prescriptionService.AddPrescription(prescriptions);
            return prescriptions;
        }


        [Authorize(Roles = "Doctor")]
        [Route("/Update whole Prescription")]

        [HttpPut]
        public async Task<Prescriptions> UpdatePrescription(Prescriptions prescriptions)
        {
            var prescription= await _prescriptionService.UpdatePrescription(prescriptions);
            return prescription;
        }
    }
}
