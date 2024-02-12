using AmazeCare.Contexts;
using AmazeCare.Exceptions;
using AmazeCare.Interfaces;
using AmazeCare.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazeCare.Repositories
{
    public class AppointmentRepository : IRepository<int, Appointments>
    {
        RequestTrackerContext _context;
        private readonly ILogger<AppointmentRepository> _logger;
        public AppointmentRepository(RequestTrackerContext context,
       ILogger<AppointmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Appointments> Add(Appointments item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation("Appointment added " + item.AppointmentId);
            return item;
        }
        public async Task<Appointments> Delete(int key)
        {
            var appointment = await GetAsync(key);
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
            _logger.LogInformation("Appointment deleted " + key);
            return appointment;
        }
        public async Task<Appointments> GetAsync(int key)
        {
            var appointments = await GetAsync();
            var appointment = appointments.FirstOrDefault(e => e.AppointmentId == key);
            if (appointment != null)
            {
                return appointment;
            }
            throw new NoSuchAppointmentException();
        }
        public async Task<List<Appointments>> GetAsync()
        {
            var appointment = _context.Appointments.Include(e => e.Patients).
                Include(d => d.Doctors).ToList();
            return appointment;
        }



        public async Task<Appointments> Update(Appointments item)
        {
            var appointment = await GetAsync(item.AppointmentId);
            _context.Entry<Appointments>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation("Appointment updated " + item.AppointmentId);
            return appointment;
        }
    }
}
