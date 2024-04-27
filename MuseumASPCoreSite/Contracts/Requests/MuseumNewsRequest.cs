namespace MuseumASPCoreSite.Contracts.Requests
{
    public record MuseumNewsRequest(
       int Id,
       string Title,
       string Description,
       IFormFile Image
       );
}
