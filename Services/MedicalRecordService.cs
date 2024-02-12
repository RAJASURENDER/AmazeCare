using AmazeCare.Interfaces;
using AmazeCare.Models;
using AmazeCare.Models.DTOs;
using AmazeCare.Repositories;
using System.Numerics;

namespace AmazeCare.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {

        private readonly IPatientAdminService _patientService;
        private readonly IDoctorAdminService _doctorService;
        private readonly IAppointmentAdminService _appointmentService;
       

        IRepository<int, MedicalRecords> _repo;


        public MedicalRecordService(IRepository<int, MedicalRecords> repo,
            IPatientAdminService patientService, IDoctorAdminService doctorService, IAppointmentAdminService appointmentService
           )
        {
            _repo = repo;
            _patientService = patientService;
            _doctorService = doctorService;
            _appointmentService = appointmentService;
          

        }


        public async Task<MedicalRecords> AddMedicalRecord(MedicalRecords medicalRecords)
        {
            medicalRecords = await _repo.Add(medicalRecords);
            return medicalRecords;
        }

        public async Task<MedicalRecords> GetMedicalRecordById(int id)
        {
            var medicalRecords = await _repo.GetAsync(id);
            return medicalRecords;
        }

        public async Task<List<MedicalRecords>> GetMedicalRecordList()
        {

            var medicalRecords = await _repo.GetAsync();
            return medicalRecords;
        }

        public async Task<List<PatientViewMedicalRecordDTO>> GetMedicalRecordByAppointment(int appointmentId)
        {
            var medicalRecords = await _repo.GetAsync();

            medicalRecords = medicalRecords.Where(a => a.AppointmentId == appointmentId).ToList();

            // Create a list to store the details
            List<PatientViewMedicalRecordDTO> medicalRecordDetailsList = new List<PatientViewMedicalRecordDTO>();

            foreach (var medicalRecord in medicalRecords)
            { 
                var appointment = await _appointmentService.GetAppointment(medicalRecord.AppointmentId);
                if (appointment != null)
                {
                    var patient = await _patientService.GetPatient(appointment.PatientId);
                    var doctor = await _doctorService.GetDoctor(appointment.DoctorId);




                    // Create a DTO (Data Transfer Object) to store the details
                    var medicalRecordDetails = new PatientViewMedicalRecordDTO
                    {

                        PatientName = patient.PatientName,
                        DoctorName = doctor.DoctorName,
                        RecordId = medicalRecord.RecordId,
                        CurrentSymptoms = medicalRecord.CurrentSymptoms,
                        PhysicalExamination = medicalRecord.PhysicalExamination,
                        TreatmentPlan = medicalRecord.TreatmentPlan,
                        RecommendedTests = medicalRecord.RecommendedTests,
                        AppointmentId = medicalRecord.AppointmentId

                    };

                    medicalRecordDetailsList.Add(medicalRecordDetails);
                }

                
            }
            return medicalRecordDetailsList;
        }


        public async Task<List<PatientViewMedicalRecordDTO>> GetMedicalRecordByPatientId(int patientId)
        {
            // Retrieve all appointments for the patient
            var appointments = await _appointmentService.GetAppointmentByPatient(patientId);

            // Create a list to store the details
            List<PatientViewMedicalRecordDTO> medicalRecordDetailsList = new List<PatientViewMedicalRecordDTO>();

            foreach (var appointment in appointments)
            {
                // Retrieve all medical records associated with each appointment
                var medicalRecords = await GetMedicalRecordByAppointment(appointment.AppointmentId);

                foreach (var medicalRecord in medicalRecords)
                {
                    var patient = await _patientService.GetPatient(patientId);
                    var doctor = await _doctorService.GetDoctor(appointment.DoctorId);
                   

                    // Create a DTO (Data Transfer Object) to store the details
                    var medicalRecordDetails = new PatientViewMedicalRecordDTO
                    {
                        PatientName = patient.PatientName,
                        DoctorName = doctor.DoctorName,
                        RecordId = medicalRecord.RecordId,
                        CurrentSymptoms = medicalRecord.CurrentSymptoms,
                        PhysicalExamination = medicalRecord.PhysicalExamination,
                        TreatmentPlan = medicalRecord.TreatmentPlan,
                        RecommendedTests = medicalRecord.RecommendedTests
                        
                    };

                    medicalRecordDetailsList.Add(medicalRecordDetails);
                }
            }

            return medicalRecordDetailsList;
        }
    }
}












