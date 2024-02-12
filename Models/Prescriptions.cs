using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AmazeCare.Models
{
    public class Prescriptions : IEquatable<Prescriptions>
    {
        [Key]
        public  int PrescriptionId { get; set; }
        public int RecordId { get; set; }
        public string Medicine { get; set; } = string.Empty;

        public string Instructions { get; set; } = string.Empty;

        public string Dosage { get; set; } = string.Empty;

        [ForeignKey("RecordId")]
        [JsonIgnore]
        public MedicalRecords? MedicalRecords { get; set; }

        public Prescriptions()
        {
            
        }

        public Prescriptions(int prescriptionId, int recordId, string medicine, string instructions, string dosage)
        {
            PrescriptionId = prescriptionId;
            RecordId = recordId;
            Medicine = medicine;
            Instructions = instructions;
            Dosage = dosage;
        }
        public Prescriptions(int recordId, string medicine, string instructions, string dosage)
        {
            
            RecordId = recordId;
            Medicine = medicine;
            Instructions = instructions;
            Dosage = dosage;
        }
        public bool Equals(Prescriptions? other)
        {
            var prescription = other ?? new Prescriptions();
            return this.PrescriptionId.Equals(prescription.PrescriptionId);
        }
    }
}
