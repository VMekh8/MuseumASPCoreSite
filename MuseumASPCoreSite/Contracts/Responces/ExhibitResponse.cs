namespace MuseumASPCoreSite.Contracts
{
    public record ExhibitResponse(
        int Id,
        string Title,
        string Description,
        string Image,
        int? ExhibitionId
        );
}
