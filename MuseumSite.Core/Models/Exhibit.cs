namespace MuseumSite.Core.Models
{
    public class Exhibit
    {
        public const int MAX_TITLE_LENGHT = 50;
        public const int MAX_DESCRIPTION_LENGHT = 500;


        public Guid Id { get; }

        public string Title { get; } = string.Empty;

        public string Description { get; } = string.Empty;

        public byte[] Image { get; }

        public Guid ExhibitionId { get; }

        public Exhibition Exhibition { get; }


        private Exhibit(Guid id, string title, string desc, byte[] image)
        {
            Id = id;
            Title = title;
            Description = desc;
            Image = image;
        }

        public static (Exhibit Exhibit, string Error) CreateExhibit(Guid id, string title, string desc, byte[] image)
        {
            var Error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGHT)
            {
                Error = "Title format error";
            }
            else if (string.IsNullOrEmpty(desc) || desc.Length > MAX_DESCRIPTION_LENGHT)
            {
                Error = "Description format error";
            }

            var exhibit = new Exhibit(id, title, desc, image);

            return (exhibit, Error);
        }

    }
}
