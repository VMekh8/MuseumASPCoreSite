using MuseumSite.Core.Models;

namespace MuseumSite.Application.Services
{
    public interface IMuseumNewsService
    {
        Task<Guid> CreateNewsAsync(MuseumNews museumNews);
        Task<Guid> DeleteNewsAsync(Guid id);
        Task<List<MuseumNews>> GetAllNewsAsync();
        Task<MuseumNews> GetNewsByNameAsync(string name);
        Task<Guid> UpdateNewsAsync(MuseumNews news);
    }
}