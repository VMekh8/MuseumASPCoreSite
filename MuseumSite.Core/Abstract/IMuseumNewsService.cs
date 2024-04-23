using MuseumSite.Core.Models;

namespace MuseumSite.Application.Services
{
    public interface IMuseumNewsService
    {
        Task<Guid> CreateNewsAsync(MuseumNews museumNews);
        Task<Guid> DeleteNewsAsync(Guid id);
        Task<List<MuseumNews>> GetAllNewsAsync();
        Task<MuseumNews> GetNewsByIdAsync(Guid id);
        Task<Guid> UpdateNewsAsync(MuseumNews news);
    }
}