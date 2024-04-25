namespace MuseumASPCoreSite.Contracts
{
    public record ExhibitResponce(
        Guid Id,
        string Title,
        string Description,
        string Image,
        Guid? ExhibitionId
        );
}
