namespace MuseumSite.Core.Models
{
    public class MuseumNews
    {
        public Guid Id { get;}
        public string Title { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public byte[] Image { get;}

        private MuseumNews(Guid id, string title, string desc, byte[] img)
        {
            Id = id;
            Title = title;
            Description = desc;
            Image = img;
        }

        public static (MuseumNews News, string Error) CreateNews(Guid id, string title, string desc, byte[] img)
        {
            string Error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > Exhibit.MAX_TITLE_LENGHT)
            {
                Error = "Title format error";
            }
            else if (string.IsNullOrEmpty(desc) || desc.Length > Exhibit.MAX_DESCRIPTION_LENGHT)
            {
                Error = "Description format error";
            }

            var news = new MuseumNews(id, title, desc, img);
            return (news, Error);

        }
    }
}
