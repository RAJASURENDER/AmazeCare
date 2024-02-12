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
    public class AppointmentController : ControllerBase
    {
        IAppointmentAdminService _appointmentAdminService;
        IAppointmentUserService _appointmentUserService;
        public AppointmentController(IAppointmentAdminService appointmentAdminService,
       IAppointmentUserService appointmentUserService)
        {
            _appointmentAdminService = appointmentAdminService;
            _appointmentUserService = appointmentUserService;
        }

        /// <summary>
        /// Retrieve all appointments (for Admin only).
        /// </summary>
        /// <returns>All the appointments/returns>

        [Authorize(Roles = "Admin")]
        
        [Route("View All The Appointments")] //admin
        [HttpGet]
        public async Task<List<Appointments>> GetAppointment()
        {
            var appointment = await _appointmentAdminService.GetAppointmentList();
            return appointment;
        }
        /// <summary>
        /// Gets an appointment by its id.
        /// </summary>
        /// <param name="id">The ID of the doctor to retrieve.</param>
        /// <returns>The appointment with the specified id.</returns>
        [Authorize]
        [Route("/View Appointment By AppointmentId")]
        [HttpGet]
        public async Task<Appointments> GetAppointmentById(int id)
        {
            var appointment = await _appointmentUserService.GetAppointment(id);
            return appointment;
        }
        /// <summary>
        /// Books an appointment for a patient.
        /// </summary>
        /// <param name="appointment">The appointment to book.</param>
        /// <returns>The booked appointment.</returns>

        [Authorize(Roles = "Patient")]
        [Route("/Book An Appointment")]
        [HttpPost]
        public async Task<Appointments> PostAppointment(Appointments appointment)
        {
            appointment = await _appointmentAdminService.AddAppointment(appointment);
            return appointment;
        }

        /// <summary>
<<<<<<< HEAD
        /// Updates the doctor id in an appointment.
        /// </summary>
        /// <param name="appointmentDTO">The appointment data transfer object.</param>
        /// <returns>The updated appointment.</returns>

=======
        /// This method is used to update DoctorId in Appointments
        /// </summary>
        /// <param name="appointmentDTO"></param>
        /// <returns> Appointment with updated DoctorId</returns>
     
>>>>>>> dd4b272acb48671b91d7ab6cd129ae408cb07a26
        [Authorize(Roles = "Admin")]
        [Route("/Update DoctorId In Appointments")]
        [HttpPut]
        public async Task<Appointments> UpdateAppointmentDoctor(AppointmentDoctorDTO appointmentDTO)
        {
            var appointment = await
           _appointmentAdminService.UpdateAppointmentDoctor(appointmentDTO.Id,
           appointmentDTO.DoctorId);
            return appointment;
        }


        /// <summary>
        /// Reschedules an appointment.
        /// </summary>
        /// <param name="appointmentDTO">The appointment data transfer object.</param>
        /// <returns>The updated appointment.</returns>

        [Authorize]
        [Route("/Reschedule Appointment")]
        [HttpPut]
        public async Task<Appointments> UpdateAppointmentDate(AppointmentDateDTO
      appointmentDTO)
        {
            var appointment = await
           _appointmentAdminService.UpdateAppointmentDate(appointmentDTO.Id,
           appointmentDTO.AppointmentDate);
            return appointment;
        }

        // [Route("/Update PatientId In Appointments")] //should look into this method
        // [HttpPut]
        // public async Task<Appointments> UpdateAppointmentPatient(AppointmentPatientDTO
        //appointmentDTO)
        // {
        //     var appointment = await
        //    _appointmentAdminService.UpdateAppointmentPatient(appointmentDTO.Id,
        //    appointmentDTO.PatientId);
        //     return appointment;
        // }


        
        [Authorize(Roles ="Patient")]
        [Route("/Cancel Appointment")] //patient
        [HttpPut]
        public async Task<Appointments> CancelAppointment(int id)
        {
            var appointment = await _appointmentAdminService.CancelAppointment(id);
            return appointment;
        }


        /// <summary>
        /// Gets all appointments for a doctor.
        /// </summary>
        /// <param name="doctorId">The id of the doctor.</param>
        /// <returns>A list of appointments for the doctor.</returns>
        /// 

        [Authorize(Roles = "Admin,Doctor")]
        [Route("/View Appointments By DoctorId")]
        [HttpGet]
        public async Task<List<DoctorViewAppointmentDTO>> GetAppointmentByDoctor(int doctorId)
        {
            
            var appointmentDetailsList = await _appointmentAdminService.GetAppointmentByDoctor(doctorId);
            return appointmentDetailsList;

        }

        /// <summary>
        /// Gets all appointments for a patient.
        /// </summary>
        /// <param name="patientId">The id of the patient.</param>
        /// <returns>A list of appointments for the patient.</returns>

        [Authorize(Roles = "Admin,Patient")]
        [Route("/View Appointments By PatientId")]
        [HttpGet]
        public async Task<List<PatientViewAppointmentDTO>> GetAppointmentByPatient(int patientId)
        {

            var appointmentDetailsList = await _appointmentAdminService.GetAppointmentByPatient(patientId);
            return appointmentDetailsList;

        }

        /// <summary>
        /// Gets all Upcoming appointments.
        /// </summary>
        
        /// <returns>A list of the Upcoming appointments</returns>

        [Authorize(Roles = "Admin")]
        [Route("/View All UPCOMING Appointments")]
        [HttpGet]
        public async Task<List<Appointments>> GetUpcomingAppointments()
        {
            var appointment = await _appointmentAdminService.GetUpcomingAppointments();
            return appointment;
        }


        /// <summary>
        /// Changing the Status of an Appointment.
        /// </summary>
        /// <param name="appointmentDTO">The id of the patient.</param>
        /// <returns>A list of appointments for the patient.</returns>

        [Authorize(Roles = "Admin,Doctor")]
        [Route("/CHANGE APPOINTMENT STATUS")]
        [HttpPut]
        public async Task<Appointments> UpdateAppointmentStatus(AppointmentStatusDTO
     appointmentDTO)
        {
            var appointment = await _appointmentAdminService.UpdateAppointmentStatus(appointmentDTO.Id,
           appointmentDTO.Status);
            return appointment;
        }
    }
}
