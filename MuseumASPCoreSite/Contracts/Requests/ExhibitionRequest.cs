namespace MuseumASPCoreSite.Contracts.Requests
{
    public record ExhibitionRequest(
        int Id,
        string Name,
        string Description,
        DateTime EventDate,
        IFormFile Image
        );
}
