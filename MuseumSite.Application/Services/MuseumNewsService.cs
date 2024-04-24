using MuseumSite.Core.Abstract;
using MuseumSite.Core.Models;
using MuseumSite.Domain.Repository;

namespace MuseumSite.Application.Services
{
    public class MuseumNewsService : IMuseumNewsService
    {
        private readonly IRepository<MuseumNews> _newsRepository;

        public MuseumNewsService(IRepository<MuseumNews> newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<Guid> CreateNewsAsync(MuseumNews museumNews)
        {
            return await _newsRepository.Create(museumNews);
        }

        public async Task<Guid> DeleteNewsAsync(Guid id)
        {
            return await _newsRepository.Delete(id);
        }

        public async Task<List<MuseumNews>> GetAllNewsAsync()
        {
            return await _newsRepository.GetAllItems();
        }

        public async Task<MuseumNews> GetNewsByNameAsync(string name)
        {
            return await _newsRepository.GetItemByName(name);
        }

        public async Task<Guid> UpdateNewsAsync(MuseumNews news)
        {
            return await _newsRepository.Update(news);
        }
    }
}
