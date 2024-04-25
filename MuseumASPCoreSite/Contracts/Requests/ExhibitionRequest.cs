namespace MuseumASPCoreSite.Contracts.Requests
{
    public record ExhibitionRequest(
        string Name,
        string Description,
        string Image,
        List<ExhibitResponce> Exhibits
        );
}
