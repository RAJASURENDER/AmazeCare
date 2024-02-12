using AmazeCare.Exceptions;
using AmazeCare.Interfaces;
using AmazeCare.Models;

namespace AmazeCare.Services
{
    public class PatientService : IPatientAdminService, IPatientUserService
    {
        
        IRepository<int, Patients> _repository;
        public PatientService(IRepository<int, Patients> repository)
        {
            _repository = repository;
        }
        public async Task<Patients> AddPatient(Patients patient)
        {
            patient = await _repository.Add(patient);
            return patient;
        }
        public async Task<Patients> DeletePatient(int id)
        {
            var patient = await GetPatient(id);
            if (patient != null)
            {
                patient = await _repository.Delete(id);
                return patient;
            }
            throw new NoSuchPatientException();
        }
        public async Task<Patients> GetPatient(int id)
        {
            var patient = await _repository.GetAsync(id);
            return patient;
        }
        public async Task<List<Patients>> GetPatientList()
        {
            var patients = await _repository.GetAsync();
            return patients;
        }
        public async Task<Patients> UpdatePatientAge(int id, int age)
        {
            var patient = await _repository.GetAsync(id);
            if (patient != null)
            {
                patient.Age = age;
                _repository.Update(patient);
                return patient;
            }
            return null;
        }


        public async Task<Patients> UpdatePatientMobile(int id, string mobile)
        {
            var patient = await _repository.GetAsync(id);
            if (patient != null)
            {
                patient.ContactNumber = mobile;
                _repository.Update(patient);
                return patient;
            }
            return null;
        }


        public async Task<Patients> UpdatePatient(Patients item)
        {
            Patients existingPatient = await _repository.GetAsync(item.PatientId);

            if (existingPatient != null)
            {
                // Update the properties of the existing doctor with the values from the input item
                existingPatient.PatientName = item.PatientName;
                existingPatient.Age = item.Age;
                existingPatient.Gender = item.Gender;
                existingPatient.DateOfBirth = item.DateOfBirth;
                existingPatient.ContactNumber = item.ContactNumber;

                await _repository.Update(existingPatient);  // Assuming your repository has an Update method

                return existingPatient;
            }
            throw new NoSuchPatientException();
        }


    }
}
