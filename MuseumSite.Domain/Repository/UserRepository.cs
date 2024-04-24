using Microsoft.AspNetCore.Identity;
using MuseumSite.Core.Abstract;
using MuseumSite.Core.Models;
using MuseumSite.Domain.Entitites;

public class UserRepository : IRepository<UserEntity>
{
    private readonly UserManager<UserEntity> _userManager;

    public UserRepository(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Guid> Create(UserEntity entity)
    {
        var result = await _userManager.CreateAsync(entity, entity.PasswordHash);

        if (result.Succeeded)
        {
            return Guid.Parse(entity.Id);
        }   

        throw new Exception("User creation failed");
    }

    public async Task<Guid> Delete(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return id;
            }
        }

        throw new Exception("User deletion failed");
    }

    public async Task<List<UserEntity>> GetAllItems()
    {
        return _userManager.Users.ToList();
    }

    public async Task<UserEntity> GetItemById(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<Guid> Update(UserEntity entity)
    {
        var result = await _userManager.UpdateAsync(entity);

        if (result.Succeeded)
        {
            return Guid.Parse(entity.Id);
        }

        throw new Exception("User update failed");
    }
}
