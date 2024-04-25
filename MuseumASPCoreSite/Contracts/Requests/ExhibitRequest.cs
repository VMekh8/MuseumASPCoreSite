namespace MuseumASPCoreSite.Contracts.Requests
{
    public record ExhibitRequest(
         string Title,
         string Description,
         string Image,
         Guid? ExhibitionId
         );
}
