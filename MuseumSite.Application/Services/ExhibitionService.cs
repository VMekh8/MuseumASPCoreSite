using MuseumSite.Core.Abstract;
using MuseumSite.Core.Models;
using MuseumSite.Domain.Repository;

namespace MuseumSite.Application.Services
{
    public class ExhibitionService : IExhibitionService
    {
        private readonly IExhibitionInterface _exhibitionRepository;

        public ExhibitionService(IExhibitionInterface exhibitionRepository)
        {
            _exhibitionRepository = exhibitionRepository;
        }

        public async Task<int> CreateExhibitionAsync(Exhibition exhibition)
        {
            return await _exhibitionRepository.Create(exhibition);
        }

        public async Task<int> DeleteExhibitionAsync(int id)
        {
            return await _exhibitionRepository.Delete(id);
        }

        public async Task<int> UpdateExhibitionAsync(Exhibition exhibition)
        {
            return await _exhibitionRepository.Update(exhibition);
        }

        public async Task<List<Exhibition>> GetExhibitionsAsync()
        {
            return await _exhibitionRepository.GetAllItems();
        }

        public async Task<Exhibition> GetExhibitionByNameAsync(string name)
        {
            return await _exhibitionRepository.GetItemByName(name);
        }

        public async Task<List<Exhibit>> GetExhibitsOnExhibitionAsync(string name)
        {
            return await _exhibitionRepository.GetExhibitOnExhibitionByName(name);
        }

        public async Task<int> AddExhibitToExhibition(int exhibitionId, int exhibitId)
        {
            return await _exhibitionRepository.AddExhibitToExhibition(exhibitionId, exhibitId);
        }

        public async Task<int> DeleteExhibitFromExhibition(int exhibitionId, int exhibitId)
        {
            return await _exhibitionRepository.DeleteExhibitFromExhibition(exhibitionId, exhibitId);
        }



    }
}
