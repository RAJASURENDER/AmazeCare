using AmazeCare.Exceptions;
using AmazeCare.Interfaces;
using AmazeCare.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazeCare.Services
{

    public class PrescriptionService : IPrescriptionService
    {
        IRepository<int, Prescriptions> _repo;
        IPrescriptionRepository _repos;

       

        public PrescriptionService(IRepository<int, Prescriptions> repo, IPrescriptionRepository repos)
        {
            _repo = repo;
            _repos = repos;
        }
        public async Task<Prescriptions> AddPrescription(Prescriptions prescriptions)
        {
            prescriptions = await _repo.Add(prescriptions);
            return prescriptions;
        }

        public async Task<List<Prescriptions>> GetPrescriptionList()
        {
            var prescription = await _repo.GetAsync();
            return prescription;

        }

        public async Task<Prescriptions> GetPrescriptionById(int id)
        {
            var prescriptions = await _repo.GetAsync(id);
            return prescriptions;
        }



        public async Task<Prescriptions> UpdatePrescription(Prescriptions item)
        {
            Prescriptions existingPrescription = await _repo.GetAsync(item.PrescriptionId);

            if (existingPrescription != null)
            {
                // Update the properties of the existing doctor with the values from the input item
                existingPrescription.Medicine = item.Medicine;
                existingPrescription.RecordId = item.RecordId;
                existingPrescription.Instructions = item.Instructions;
                existingPrescription.Dosage = item.Dosage;


                await _repo.Update(existingPrescription); 

                return existingPrescription;
            }
            throw new NoSuchPrescriptionException();
        }

        public async Task<List<Prescriptions>> GetPrescriptionsByRecordId(int recordId)
        {
            var prescriptions = await _repos.GetByRecordIdAsync(recordId);
            return prescriptions;
        }



    }
}
