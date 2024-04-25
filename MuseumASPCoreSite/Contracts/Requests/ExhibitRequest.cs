namespace MuseumASPCoreSite.Contracts.Requests
{
    public record ExhibitResponce(
         string Title,
         string Description,
         string Image,
         Guid? ExhibitionId
         );
}
