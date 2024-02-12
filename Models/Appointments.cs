using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AmazeCare.Models
{
    public class Appointments : IEquatable<Appointments>
    {
        [Key]
        public int AppointmentId { get; set; }

        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public DateTime AppointmentDate { get; set; }
        public string? SymptomsDescription { get; set; }

        public string? Status { get; set; }
        public string? NatureOfVisit { get; set; }


        [ForeignKey("DoctorId")]
        [JsonIgnore]
        public Doctors? Doctors { get; set; }


        [ForeignKey("PatientId")]
        [JsonIgnore]
        public Patients? Patients { get; set; }

        [JsonIgnore]
        public MedicalRecords? MedicalRecords { get; set; }
 

        public Appointments()
        {
            
        }

        public Appointments(int appointmentId, int doctorId, string symptomsDescription,string status, int patientId, DateTime appointmentDate, string natureOfVisit)
        {
            AppointmentId = appointmentId;
            DoctorId = doctorId;
            SymptomsDescription = symptomsDescription;
            
            PatientId = patientId;
            Status = status;
            AppointmentDate = appointmentDate;
            NatureOfVisit = natureOfVisit;
        }

        public Appointments( int doctorId, string symptomsDescription,  int patientId, string status, DateTime appointmentDate, string natureOfVisit)
        {
           
            DoctorId = doctorId;
            SymptomsDescription = symptomsDescription;
           
            PatientId = patientId;
            Status = status;
            AppointmentDate = appointmentDate;
            NatureOfVisit = natureOfVisit;
        }
        public bool Equals(Appointments? other)
            {
                var appointments = other ?? new Appointments();
                return this.AppointmentId.Equals(appointments.AppointmentId);
            }
        }
    }

