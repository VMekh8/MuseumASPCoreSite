using Microsoft.AspNetCore.Identity;

namespace MuseumSite.Core.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get;} = string.Empty;
        public string LastName { get;} = string.Empty;

        private User(string email, string password, string? phoneNumber, string firstname, string lastname)
        {
            Email = email;
            PasswordHash = password;
            PhoneNumber = phoneNumber;
            FirstName = firstname;
            LastName = lastname;
        }

        public static (User User, string Error) CreateUser(string email, string password, string? phoneNumber, string firstname, string lastname)
        {
            string Error = string.Empty;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Error = "User data error, check for login or password";
            }
            else if (string.IsNullOrEmpty(firstname))
            {
                Error = "Name format error";
            }
            else if (string.IsNullOrEmpty(lastname))
            {
                Error = "Surname format error";
            }

            var user = new User(email, password, phoneNumber, firstname, lastname);
            return (user, Error);
        }

    }
}
