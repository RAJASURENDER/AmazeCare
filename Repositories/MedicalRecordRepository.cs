using AmazeCare.Contexts;
using AmazeCare.Exceptions;
using AmazeCare.Interfaces;
using AmazeCare.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazeCare.Repositories
{
    public class MedicalRecordRepository : IRepository<int,MedicalRecords>
    {

        RequestTrackerContext _context;
        ILogger<MedicalRecordRepository> _logger;
        public MedicalRecordRepository(RequestTrackerContext context, ILogger<MedicalRecordRepository>
       logger)
        {
            _context = context;
            _logger = logger;
        }
        public async  Task<MedicalRecords> Add(MedicalRecords item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation("MedicalRecord added " + item.RecordId);
            return item;
        }


        public async Task<List<MedicalRecords>> GetAsync()
        {
            var medicalRecords = _context.MedicalRecords.ToList();
            return medicalRecords;
        }



        public async Task<MedicalRecords> Delete(int key)
        {
            var medicalRecord = await GetAsync(key);
            _context.MedicalRecords.Remove(medicalRecord);
            _context.SaveChanges();
            _logger.LogInformation("MedicalRecord deleted " + key);
            return medicalRecord ;
        }

        public async Task<MedicalRecords> GetAsync(int key)
        {
            var medicalRecords = await GetAsync();
            var medicalRecord = medicalRecords.FirstOrDefault(e => e.RecordId == key);
            if (medicalRecord != null)
            {
                return medicalRecord;
            }
            throw new NoSuchMedicalRecordException();
        }


       

        public async Task<MedicalRecords> Update(MedicalRecords item)
        {
            var medicalRecords = await GetAsync(item.RecordId);
            _context.Entry<MedicalRecords>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation("MedicalRecords updated " + item.RecordId);
            return medicalRecords;
        }
    }
}
