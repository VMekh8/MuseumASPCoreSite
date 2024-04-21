using Microsoft.AspNetCore.Identity;

namespace MuseumSite.Domain.Entitites
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
