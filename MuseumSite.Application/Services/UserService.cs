using MuseumSite.Core.Abstract;
using MuseumSite.Core.Models;

namespace MuseumSite.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> CreateUserAsync(User user)
        {
            return await _userRepository.Create(user);
        }

        public async Task<Guid> DeleteUserAsync(Guid id)
        {
            return await _userRepository.Delete(id);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _userRepository.GetAllItems();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetItemById(id);
        }

        public async Task<Guid> UpdateUserAsync(User user)
        {
            return await _userRepository.Update(user);
        }
    }
}
