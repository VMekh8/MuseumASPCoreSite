namespace MuseumASPCoreSite.Contracts.Requests
{
    public record ExhibitRequest(
         int Id,
         string Title,
         string Description,
         IFormFile Image,
         int? ExhibitionId
         );
}
