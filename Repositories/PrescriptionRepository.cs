using AmazeCare.Contexts;
using AmazeCare.Exceptions;
using AmazeCare.Interfaces;
using AmazeCare.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazeCare.Repositories
{
    public class PrescriptionRepository :IRepository<int, Prescriptions>, IPrescriptionRepository
    {

        RequestTrackerContext _context;
        ILogger<PrescriptionRepository> _logger;
        public PrescriptionRepository(RequestTrackerContext context, ILogger<PrescriptionRepository>
       logger)
        {
            _context = context;
            _logger = logger;
        }

        public async  Task<Prescriptions> Add(Prescriptions item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation("Prescription added " + item.PrescriptionId);
            return item;
        }

        public async Task<Prescriptions> Delete(int key)
        {
            var prescription = await GetAsync(key);
            _context.Prescriptions.Remove(prescription);
            _context.SaveChanges();
            _logger.LogInformation("Prescription deleted " + key);
            return prescription;
        }

        public async Task<Prescriptions> GetAsync(int key)
        {
            var prescriptions = await GetAsync();
            var prescription = prescriptions.FirstOrDefault(e => e.RecordId == key);
            if (prescriptions!= null)
            {
                
                return prescription;

            }
            throw new NoSuchPrescriptionException();
        }

        public async Task<List<Prescriptions>> GetAsync()
        {
            var prescriptions = _context.Prescriptions.ToList();
            return prescriptions;

       
        }

        public async Task<List<Prescriptions>> GetByRecordIdAsync(int recordId)
        {
            var prescriptions = _context.Prescriptions
                            .Where(p => p.RecordId == recordId)
                            .ToList();

            return prescriptions;
        }

        public async Task<Prescriptions> Update(Prescriptions item)
        {
            var prescription = await GetAsync(item.PrescriptionId);
            _context.Entry<Prescriptions>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation("Prescription updated " + item.PrescriptionId);
            return prescription;
        }
    }

}

