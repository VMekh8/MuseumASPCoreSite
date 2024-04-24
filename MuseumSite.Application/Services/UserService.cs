using MuseumSite.Core.Abstract;
using MuseumSite.Domain.Entitites;

namespace MuseumSite.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _userRepository;

        public UserService(IRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> CreateUserAsync(UserEntity user)
        {
            return await _userRepository.Create(user);
        }

        public async Task<Guid> DeleteUserAsync(Guid id)
        {
            return await _userRepository.Delete(id);
        }

        public async Task<List<UserEntity>> GetUsersAsync()
        {
            return await _userRepository.GetAllItems();
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetItemById(id);
        }

        public async Task<Guid> UpdateUserAsync(UserEntity user)
        {
            return await _userRepository.Update(user);
        }
    }
}
