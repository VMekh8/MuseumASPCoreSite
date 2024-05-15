namespace MuseumASPCoreSite.Contracts
{
    public record ExhibitionResponse(
        int Id,
        string Name, 
        string Description,
        string Image,
        DateTime Date,
        List<ExhibitResponse> Exhibits
        );
}
