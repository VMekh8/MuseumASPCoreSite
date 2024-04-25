namespace MuseumSite.Domain.Entitites
{
    public class MuseumNewsEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public byte[] Image { get; set; }
    }
}
