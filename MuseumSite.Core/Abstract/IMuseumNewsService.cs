using MuseumSite.Core.Models;

namespace MuseumSite.Application.Services
{
    public interface IMuseumNewsService
    {
        Task<int> CreateNewsAsync(MuseumNews museumNews);
        Task<int> DeleteNewsAsync(int id);
        Task<List<MuseumNews>> GetAllNewsAsync();
        Task<MuseumNews> GetNewsByNameAsync(string name);
        Task<int> UpdateNewsAsync(MuseumNews news);
    }
}