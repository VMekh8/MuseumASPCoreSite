using MuseumSite.Core.Models;

namespace MuseumSite.Application.Services
{
    public interface IUserService
    {
        Task<Guid> CreateUserAsync(User user);
        Task<Guid> DeleteUserAsync(Guid id);
        Task<User> GetUserByIdAsync(Guid id);
        Task<List<User>> GetUsersAsync();
        Task<Guid> UpdateUserAsync(User user);
    }
}