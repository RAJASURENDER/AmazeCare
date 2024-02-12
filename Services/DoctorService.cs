using AmazeCare.Exceptions;
using AmazeCare.Interfaces;
using AmazeCare.Models;
using System.Numerics;

namespace AmazeCare.Services
{
    public class DoctorService : IDoctorAdminService, IDoctorUserService
    {
        IRepository<int, Doctors> _repo;
        public DoctorService(IRepository<int, Doctors> repo)
        {
            _repo = repo;
        }


        public async Task<Doctors> AddDoctor(Doctors doctor)
        {
            doctor = await _repo.Add(doctor);
            return doctor;
        }
        public async Task<Doctors> DeleteDoctor(int id)
        {
            var doctor = await GetDoctor(id);
            if (doctor != null)
            {
                doctor = await _repo.Delete(id);
                return doctor;
            }
            throw new NoSuchDoctorException();
        }
        public async Task<Doctors> GetDoctor(int id)
        {
            var doctor = await _repo.GetAsync(id);
            return doctor;
        }

        public async Task<List<Doctors>> GetDoctorList()
        {
            var doctor = await _repo.GetAsync();
            return doctor;
        }

        public async Task<Doctors> UpdateDoctorExperience(int id, float experience)
        {
            var doctor = await _repo.GetAsync(id);
            if (doctor != null)
            {
                doctor.Experience = experience;
                doctor = await _repo.Update(doctor);
                return doctor;
            }
            return null;
        }

        public async Task<Doctors> UpdateDoctorQualification(int id, string qualification)
        {
            var doctor = await _repo.GetAsync(id);
            if (doctor != null)
            {
                doctor.Qualification = qualification;
                doctor = await _repo.Update(doctor);
                return doctor;
            }
            return null;
        }


        public async Task<Doctors> UpdateDoctor(Doctors item)
        {
            Doctors existingDoctor = await _repo.GetAsync(item.DoctorId);

            if (existingDoctor != null)
            {
                // Update the properties of the existing doctor with the values from the input item
                existingDoctor.DoctorName = item.DoctorName;
                existingDoctor.Qualification = item.Qualification;
                existingDoctor.Speciality = item.Speciality;
                existingDoctor.Designation = item.Designation;
                existingDoctor.Experience = item.Experience;

                await _repo.Update(existingDoctor);  // Assuming your repository has an Update method

                return existingDoctor;
            }
            throw new NoSuchDoctorException();
        }

    }
}
