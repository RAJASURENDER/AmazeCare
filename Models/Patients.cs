using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AmazeCare.Models
{
    public class Patients : IEquatable<Patients>
    {
        [Key]
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;

        public int Age { get; set; }

        public string? Gender { get; set; }

        public DateTime DateOfBirth  { get; set; }

        public string? ContactNumber { get; set; }

        public string Username { get; set; } = string.Empty;

        [ForeignKey("Username")]
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public ICollection<Appointments>? Appointments { get; set; }





        public Patients()
        {
            
        }

        public Patients(int patientId, string patientName, int age, string? gender, DateTime dateOfBirth, string contactNumber, string username)
        {
            PatientId = patientId;
            PatientName = patientName;
            Age = age;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            ContactNumber = contactNumber;
            Username = username;
        }

        public Patients( string patientName, int age, string? gender, DateTime dateOfBirth,string contactNumber, string username)
        {
          
            PatientName = patientName;
            Age = age;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            ContactNumber = contactNumber;
            Username = username;
        }


        public bool Equals(Patients? other)
        {
            var patients = other ?? new Patients();
            return this.PatientId.Equals(patients.PatientId);
        }
    }
}

