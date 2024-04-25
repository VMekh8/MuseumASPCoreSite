using MuseumSite.Domain.Entitites;

namespace MuseumSite.Application.Services
{
    public interface IUserService
    {
        Task<int> CreateUserAsync(UserEntity user);
        Task<int> DeleteUserAsync(int id);
        Task<UserEntity> GetUserByNameAsync(string name);
        Task<List<UserEntity>> GetUsersAsync();
        Task<int> UpdateUserAsync(UserEntity user);
    }
}