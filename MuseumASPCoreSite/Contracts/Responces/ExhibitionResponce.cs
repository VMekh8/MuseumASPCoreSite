namespace MuseumASPCoreSite.Contracts
{
    public record ExhibitionResponce(
        int Id,
        string Name, 
        string Description,
        string Image,
        DateTime? Date,
        List<ExhibitResponce> Exhibits
        );
}
