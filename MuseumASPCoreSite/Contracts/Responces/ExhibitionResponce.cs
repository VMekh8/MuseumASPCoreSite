namespace MuseumASPCoreSite.Contracts
{
    public record ExhibitionResponce(
        Guid Id,
        string Name, 
        string Description,
        string Image,
        List<ExhibitResponce> Exhibits
        );
}
