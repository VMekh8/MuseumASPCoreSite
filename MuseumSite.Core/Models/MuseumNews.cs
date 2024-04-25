namespace MuseumSite.Core.Models
{
    public class MuseumNews
    {
        public const int MAX_NEWS_TITLE_LENGHT = 100, MAX_NEWS_DESCRIPTION_LENGHT = 500;

        public int Id { get;}
        public string Title { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public byte[] Image { get;}

        private MuseumNews(int id, string title, string desc, byte[] img)
        {
            Id = id;
            Title = title;
            Description = desc;
            Image = img;
        }

        public static (MuseumNews News, string Error) CreateNews(int id, string title, string desc, byte[] img)
        {
            string Error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > MAX_NEWS_TITLE_LENGHT)
            {
                Error = "Title format error";
            }
            else if (string.IsNullOrEmpty(desc) || desc.Length > MAX_NEWS_DESCRIPTION_LENGHT)
            {
                Error = "Description format error";
            }

            var news = new MuseumNews(id, title, desc, img);
            return (news, Error);

        }
    }
}
