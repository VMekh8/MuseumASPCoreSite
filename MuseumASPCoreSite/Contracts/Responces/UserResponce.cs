namespace MuseumASPCoreSite.Contracts
{
    public record UserResponce(
        string id,
        string Email, 
        string Password, 
        string?PhoneNumber, 
        string Firstname,
        string Lastname
        );
}
