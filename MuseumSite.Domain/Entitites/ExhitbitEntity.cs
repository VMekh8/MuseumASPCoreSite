namespace MuseumSite.Domain.Entitites
{
    public class ExhitbitEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public int? ExhibitionEntityId { get; set; }

        public ExhibitionEntity? ExhibitionEntity { get; set; }
    }
}
