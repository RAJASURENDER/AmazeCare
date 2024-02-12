using AmazeCare.Contexts;
using AmazeCare.Interfaces;
using AmazeCare.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazeCare.Repositories
{
    public class UserRepository : IRepository<string, User>
    {
        private readonly RequestTrackerContext _context;

        public UserRepository(RequestTrackerContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User item)
        {
            _context.Add(item);
            _context.SaveChanges();
            return item;
        }

        public async Task<User> Delete(string key)
        {
            var user = await GetAsync(key);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
                return user;
            }
            return null;
        }

        public async Task<User> GetAsync(string key)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == key);
            return user;
        }

        public async Task<List<User>> GetAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> Update(User item)
        {
            var user = await GetAsync(item.Username);
            if (user != null)
            {
                _context.Entry<User>(item).State = EntityState.Modified;
                _context.SaveChanges();
                return item;
            }
            return null;
        }
    }
}
