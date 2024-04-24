namespace MuseumSite.Core.Models
{
    public class Exhibition
    {
        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string Description { get;} = string.Empty;
        public DateTime EventDate { get;}
        public byte[] Image { get;}
        public List<Exhibit> Exhibits { get; } = new List<Exhibit>();

        private Exhibition(Guid id, string name, string desc, DateTime date, byte[] image)
        {
            Id = id;
            Name = name;
            Description = desc;
            EventDate = date;
            Image = image;
        }

        public static (Exhibition Exhibition, string Error) CreateExhibition(Guid id, string name, string desc, DateTime date, byte[] image)
        {
            var Error = string.Empty;

            if (string.IsNullOrEmpty(name) || name.Length > Exhibit.MAX_TITLE_LENGHT)
            {
                Error = "Event name format error";
            }
            else if (string.IsNullOrEmpty(desc) || desc.Length > Exhibit.MAX_DESCRIPTION_LENGHT)
            {
                Error = "Event description error";
            }
            else if (date < DateTime.Today)
            {
                Error = "Event date format error";
            }

            var exhibition = new Exhibition(id, name, desc, date, image);

            return (exhibition, Error);
        }
    }
}
