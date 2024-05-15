namespace MuseumASPCoreSite.Contracts
{
    public record MuseumNewsResponse(
        int Id,
        string Title,
        string Description,
        string Image
        );
}
