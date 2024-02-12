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

        /// <summary>
        /// Gets the list of all prescriptions.
        /// </summary>
        /// <returns>List of prescriptions</returns>

        [Authorize(Roles = "Admin")]
        [Route("View All Prescriptions")]
        [HttpGet]
        public async Task<List<Prescriptions>> GetPrescription()
        {
            var prescriptions = await _prescriptionService.GetPrescriptionList();
            return prescriptions;
        }

        /// <summary>
        /// Gets the prescription by record id.
        /// </summary>
        /// <param name="id">The id of the prescription record.</param>
        /// <returns>The prescription record</returns>

        [Authorize(Roles = "Patient")]
        [Route("/View Prescription  By RecordId")]
        [HttpGet]
        public async Task<Prescriptions> GetPrescriptionById(int id)
        {
            var prescription = await _prescriptionService.GetPrescriptionById(id);
            return prescription;
        }

        /// <summary>
        /// Adds a new prescription.
        /// </summary>
        /// <param name="prescriptions">The prescription details.</param>
        /// <returns>Added prescription details</returns>

        [Authorize(Roles = "Doctor")]
        [Route("Add Prescription")]
        [HttpPost]
        public async Task<Prescriptions> PostMedicalRecord(Prescriptions prescriptions)
        {
            prescriptions = await _prescriptionService.AddPrescription(prescriptions);
            return prescriptions;
        }

        /// <summary>
        /// Updates the prescription.
        /// </summary>
        /// <param name="prescriptions"> The Updated Prescription Details </param>
        /// <returns>The Updted Prescription Details</returns>

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
