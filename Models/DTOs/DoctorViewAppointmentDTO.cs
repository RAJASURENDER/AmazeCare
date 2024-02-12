namespace AmazeCare.Models.DTOs
{
    public class DoctorViewAppointmentDTO
    {
        public int AppointmentId { get; set; }
        public string? PatientName { get; set; }
        public string? ContactNumber { get; set; }
        public string? Symptoms { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Status { get; set; }
    }
}
