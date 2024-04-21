using Microsoft.AspNetCore.Identity;

namespace MuseumSite.Core.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get;} = string.Empty;
        public string LastName { get;} = string.Empty;

        private User(string name, string surname)
        {
            FirstName = name;
            LastName = surname;
        }

        public static (User User, string Error) CreateUser(string name, string lastname)
        {
            string Error = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                Error = "Name format error";
            }
            else if (string.IsNullOrEmpty(lastname))
            {
                Error = "Surname format error";
            }

            var user = new User(name, lastname);
            return (user, Error);
        }

    }
}
