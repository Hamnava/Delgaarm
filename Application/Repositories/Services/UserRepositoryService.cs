
using Application.Repositories.Interface;
using Infrastracture.Context;
using Infrastracture.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Repositories.Services
{
    public class UserRepositoryService : UserInterface
    {
        private readonly ApplicationContext _context;

        public UserRepositoryService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<ApplicationUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<ApplicationUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username);
        }

        public void UpdateUser(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
