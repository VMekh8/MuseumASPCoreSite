namespace MuseumASPCoreSite.Contracts.Requests
{
    public record ExhibitionResponce(
        string Name,
        string Description,
        string Image,
        List<ExhibitResponce> Exhibits
        );
}
