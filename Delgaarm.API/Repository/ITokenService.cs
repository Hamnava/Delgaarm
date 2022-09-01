


using Infrastracture.Entities;

namespace Application.API.Repository
{
    public interface ITokenService
    {
        Task<string> GetToken(ApplicationUser user);
    }
}
