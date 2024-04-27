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

    public async Task<int> Create(UserEntity entity)
    {
        var result = await _userManager.CreateAsync(entity, entity.PasswordHash);

        if (result.Succeeded)
        {
            return int.Parse(entity.Id);
        }   

        throw new Exception("User creation failed");
    }

    public async Task<int> Delete(int id)
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

    public async Task<UserEntity> GetItemByName(string email)
    {
        return await _userManager.FindByNameAsync(email);
    }

    public async Task<int> Update(UserEntity entity)
    {
        var result = await _userManager.UpdateAsync(entity);

        if (result.Succeeded)
        {
            return int.Parse(entity.Id);
        }

        throw new Exception("User update failed");
    }
}
