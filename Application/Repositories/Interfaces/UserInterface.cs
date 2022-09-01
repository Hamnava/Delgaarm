

using Application.ViewModels;
using Infrastracture.Entities;

namespace Application.Repositories.Interface
{
    public interface UserInterface
    {
        Task<ApplicationUser> GetUserByIdAsync(int id);
        void UpdateUser(ApplicationUser user);
        Task<ApplicationUser> GetUserByUsernameAsync(string username);
        Task<bool> SaveAllAsync();
    }
}
