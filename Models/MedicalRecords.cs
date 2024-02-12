using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AmazeCare.Models
{
    public class MedicalRecords : IEquatable<MedicalRecords>
    {
        [Key]
        public int RecordId { get; set; }

        public string CurrentSymptoms { get; set; } = string.Empty;

        public string PhysicalExamination { get; set; } = string.Empty;

        public string TreatmentPlan { get; set; } = string.Empty;

        public string RecommendedTests { get; set; } = string.Empty;

     

        public int AppointmentId { get; set; }

        [ForeignKey("AppointmentId")]
        [JsonIgnore]
        public Appointments? Appointments { get; set; }

 

        public MedicalRecords()
        {
            
        }

        public MedicalRecords(int recordId, string currentSymptoms, string physicalExamination, string treatmentPlan, string recommendedTests,  int appointmentId)
        {
            RecordId = recordId;
            CurrentSymptoms = currentSymptoms;
            PhysicalExamination = physicalExamination;
            TreatmentPlan = treatmentPlan;
            RecommendedTests = recommendedTests;
           
            AppointmentId = appointmentId;
           
       
         
           
        }

        public MedicalRecords(string currentSymptoms, string physicalExamination, string treatmentPlan, string recommendedTests,  int appointmentId)
        {
            
            CurrentSymptoms = currentSymptoms;
            PhysicalExamination = physicalExamination;
            TreatmentPlan = treatmentPlan;
            RecommendedTests = recommendedTests;
           
            AppointmentId = appointmentId;



        }

        public bool Equals(MedicalRecords? other)
        {
            var medicalrecords = other ?? new MedicalRecords();
            return this.RecordId.Equals(medicalrecords.RecordId);
        }
    }
}

