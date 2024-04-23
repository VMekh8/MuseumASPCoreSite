namespace MuseumASPCoreSite.Contracts
{
    public record UserResponce(
        string Email, 
        string Password, 
        string?PhoneNumber, 
        string Firstname,
        string Lastname
        );
}
