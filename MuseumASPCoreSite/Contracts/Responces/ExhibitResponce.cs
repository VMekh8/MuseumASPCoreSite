namespace MuseumASPCoreSite.Contracts
{
    public record ExhibitResponce(
        int Id,
        string Title,
        string Description,
        string Image,
        int? ExhibitionId
        );
}
