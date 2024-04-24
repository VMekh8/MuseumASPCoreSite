﻿using MuseumSite.Domain.Entitites;

namespace MuseumSite.Application.Services
{
    public interface IUserService
    {
        Task<Guid> CreateUserAsync(UserEntity user);
        Task<Guid> DeleteUserAsync(Guid id);
        Task<UserEntity> GetUserByNameAsync(string name);
        Task<List<UserEntity>> GetUsersAsync();
        Task<Guid> UpdateUserAsync(UserEntity user);
    }
}