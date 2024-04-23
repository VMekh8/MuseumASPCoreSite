using Microsoft.AspNetCore.Identity;
using MuseumSite.Core.Abstract;
using MuseumSite.Core.Models;

public class UserRepository : IRepository<User>
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Guid> Create(User entity)
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

    public async Task<List<User>> GetAllItems()
    {
        return _userManager.Users.ToList();
    }

    public async Task<User> GetItemById(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<Guid> Update(User entity)
    {
        var result = await _userManager.UpdateAsync(entity);

        if (result.Succeeded)
        {
            return Guid.Parse(entity.Id);
        }

        throw new Exception("User update failed");
    }
}
