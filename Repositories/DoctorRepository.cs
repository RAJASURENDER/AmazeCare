using AmazeCare.Contexts;
using AmazeCare.Exceptions;
using AmazeCare.Interfaces;
using AmazeCare.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace AmazeCare.Repositories
{
    public class DoctorRepository : IRepository<int, Doctors>
    {
     
        RequestTrackerContext _context;
        ILogger<DoctorRepository> _logger;
        public DoctorRepository(RequestTrackerContext context, ILogger<DoctorRepository>
       logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Doctors> Add(Doctors item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation("Doctor added " + item.DoctorId);
            return item;
        }

        public async Task<Doctors> Delete(int key)
        {
            var doctor = await GetAsync(key);
            _context?.Doctors.Remove(doctor);
            _context?.SaveChanges();
            _logger.LogInformation("Doctor deleted " + key);
            return doctor;
        }

        public async Task<Doctors> GetAsync(int key)
        {
            var doctors = await GetAsync();
            var doctor = doctors.FirstOrDefault(e => e.DoctorId == key);
            if (doctor != null)
            {
                return doctor;
            }
            throw new NoSuchDoctorException();
        }

        public async Task<List<Doctors>> GetAsync()
        {
            var doctors = _context.Doctors.ToList();
            return doctors;
        }

        public async Task<Doctors> Update(Doctors item)
        {
            var doctor = await GetAsync(item.DoctorId);
            _context.Entry<Doctors>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation("Doctor updated " + item.DoctorId);
            return doctor;
        }
    }
}
