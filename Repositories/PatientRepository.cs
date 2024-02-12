using AmazeCare.Contexts;
using AmazeCare.Exceptions;
using AmazeCare.Interfaces;
using AmazeCare.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazeCare.Repositories
{ 
    public class PatientRepository : IRepository<int, Patients>
    {
        RequestTrackerContext _context;
        ILogger<PatientRepository> _logger;
        public PatientRepository(RequestTrackerContext context, ILogger<PatientRepository>
       logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Patients> Add(Patients item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation("Patient added " + item.PatientId);
            return item;
        }
        public async Task<Patients> Delete(int key)
        {
            var patient = await GetAsync(key);
            _context.Patients.Remove(patient);
            _context.SaveChanges();
            _logger.LogInformation("Patient deleted " + key);
            return patient;
        }

        public async Task<Patients> GetAsync(int key)
        {
            var patients = await GetAsync();
            var patient = patients.SingleOrDefault(e => e.PatientId == key);
            if (patient != null)
            {
                return patient;
            }
            throw new NoSuchPatientException();
        }

        public async Task<List<Patients>> GetAsync()
        {
            var patient = _context.Patients.ToList();
            return patient;
        }

        public async Task<Patients> Update(Patients item)
        {
            var patient = await GetAsync(item.PatientId);
            _context.Entry<Patients>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation("Patient updated " + item.PatientId);
            return patient;
        }
    }
}


