namespace MuseumSite.Domain.Entitites
{
    public class ExhibitionEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public byte[] Image { get; set; }
        public ICollection<ExhitbitEntity> ExhitbitsEntities { get; set; }

    }
}
