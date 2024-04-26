namespace MuseumASPCoreSite.Contracts.Requests
{
    public record ExhibitionRequest(
        int Id,
        string Name,
        string Description,
        IFormFile Image,
        List<ExhibitResponce> Exhibits
        );
}
