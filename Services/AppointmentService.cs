using AmazeCare.Interfaces;
using AmazeCare.Models;
using AmazeCare.Models.DTOs;

namespace AmazeCare.Services
{
    public class AppointmentService : IAppointmentAdminService, IAppointmentUserService
    {
        
        private readonly IPatientAdminService _patientService;
        private readonly IDoctorAdminService _doctorService;


        IRepository<int, Appointments> _repo;
        

        public AppointmentService(IRepository<int, Appointments> repo, IPatientAdminService patientService, IDoctorAdminService doctorService)
        {
            _repo = repo;
            _patientService = patientService;
            _doctorService = doctorService;
        }
        public async Task<Appointments> AddAppointment(Appointments appointments)
        {
            appointments = await _repo.Add(appointments);
            return appointments;
        }
        public async Task<Appointments> DeleteAppointment(int id)
        {
            var appointments = await _repo.Delete(id);
            return appointments;
        }
        public async Task<Appointments> GetAppointment(int id)
        {
            var appointments = await _repo.GetAsync(id);
            return appointments;
        }
        public async Task<List<Appointments>> GetAppointmentList()
        {
            var appointments = await _repo.GetAsync();
            return appointments;
        }
        public async Task<Appointments> UpdateAppointmentDoctor(int appointmentId, int
       doctorId)
        {
            var appointment = await _repo.GetAsync(appointmentId);
            if (appointment != null)
            {
                appointment.DoctorId = doctorId;
                appointment = await _repo.Update(appointment);
                return appointment;
            }
            return null;
        }
        public async Task<Appointments> UpdateAppointmentPatient(int appointmentId, int
       patientId)
        {
            var appointment = await _repo.GetAsync(appointmentId);
            if (appointment != null)
            {
                appointment.PatientId = patientId;
                appointment = await _repo.Update(appointment);
                return appointment;
            }
            return null;
        }

        public async Task<Appointments> UpdateAppointmentDate(int appointmentId, DateTime appointmentDate)
        {
            var appointment = await _repo.GetAsync(appointmentId);
            if (appointment != null)
            {
                appointment.AppointmentDate = appointmentDate;

                appointment = await _repo.Update(appointment);
                return appointment;
            }
            return null;
        }


        // Add this method to AppointmentService.cs
        public async Task<List<DoctorViewAppointmentDTO>> GetAppointmentByDoctor(int doctorId)
        {
            var appointments = await _repo.GetAsync();
            appointments = appointments.Where(a => a.DoctorId == doctorId).ToList();

            // Create a list to store the details
            List<DoctorViewAppointmentDTO> appointmentDetailsList = new List<DoctorViewAppointmentDTO>();

            foreach (var appointment in appointments)
            {
                var patient = await _patientService.GetPatient(appointment.PatientId);


                // Create a DTO (Data Transfer Object) to store the details
                var appointmentDetails = new DoctorViewAppointmentDTO
                {
                    AppointmentId = appointment.AppointmentId,
                    PatientName = patient.PatientName,
                    ContactNumber = patient.ContactNumber,
                    Symptoms = appointment.SymptomsDescription,
                    AppointmentDate = appointment.AppointmentDate,
                    Status = appointment.Status


                };

                appointmentDetailsList.Add(appointmentDetails);
            }

            return appointmentDetailsList;
        }



        public async Task<List<PatientViewAppointmentDTO>> GetAppointmentByPatient(int patientId)
        {
            var appointments = await _repo.GetAsync();
            appointments = appointments.Where(a => a.PatientId == patientId).ToList();

            // Create a list to store the details
            List<PatientViewAppointmentDTO> appointmentDetailsList = new List<PatientViewAppointmentDTO>();

            foreach (var appointment in appointments)
            {
                var patient = await _patientService.GetPatient(appointment.PatientId);
                var doctor = await _doctorService.GetDoctor(appointment.DoctorId);


                // Create a DTO (Data Transfer Object) to store the details
                var appointmentDetails = new PatientViewAppointmentDTO
                {
                    AppointmentId = appointment.AppointmentId,
                    PatientName = patient.PatientName,
                    ContactNumber = patient.ContactNumber,
                    Symptoms = appointment.SymptomsDescription,
                    AppointmentDate = appointment.AppointmentDate,
                    Status = appointment.Status,
                    DoctorName= doctor.DoctorName,
                    DoctorId=doctor.DoctorId


                };

                appointmentDetailsList.Add(appointmentDetails);
            }

            return appointmentDetailsList;
        }


        public async Task<List<Appointments>> GetUpcomingAppointments()
        {
            var currentDate = DateTime.Now;

            var upcomingAppointments = await _repo.GetAsync();
            upcomingAppointments = upcomingAppointments
                .Where(appointment => appointment.AppointmentDate > currentDate && (appointment.Status == "upcoming" || appointment.Status == "RESCHEDULED"))
                .ToList();

            return upcomingAppointments;
        }

        public async Task<Appointments> UpdateAppointmentStatus(int appointmentId, string Status)
        {
            var appointment = await _repo.GetAsync(appointmentId);
            if (appointment != null)
            {
                appointment.Status = Status;

                appointment = await _repo.Update(appointment);
                return appointment;
            }
            return null;
        }
    }
}
