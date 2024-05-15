namespace MuseumASPCoreSite.Contracts
{
    public record UserResponse(
        string id,
        string Email, 
        string Password, 
        string?PhoneNumber, 
        string Firstname,
        string Lastname
        );
}
