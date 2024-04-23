using MuseumSite.Core.Models;
using MuseumSite.Domain.Repository;

namespace MuseumSite.Application.Services
{
    public class ExhibitionService : IExhibitionService
    {
        private readonly ExhibitionRepository _exhibitionRepository;

        public ExhibitionService(ExhibitionRepository exhibitionRepository)
        {
            _exhibitionRepository = exhibitionRepository;
        }

        public async Task<Guid> CreateExhibitionAsync(Exhibition exhibition)
        {
            return await _exhibitionRepository.Create(exhibition);
        }

        public async Task<Guid> DeleteExhibitionAsync(Guid id)
        {
            return await _exhibitionRepository.Delete(id);
        }

        public async Task<Guid> UpdateExhibitionAsync(Exhibition exhibition)
        {
            return await _exhibitionRepository.Update(exhibition);
        }

        public async Task<List<Exhibition>> GetExhibitionsAsync()
        {
            return await _exhibitionRepository.GetAllItems();
        }

        public async Task<Exhibition> GetExhibitionByIdAsync(Guid id)
        {
            return await _exhibitionRepository.GetItemById(id);
        }

        public async Task<List<Exhibit>> GetExhibitsOnExhibitionAsync(Guid exhibitionId)
        {
            return await _exhibitionRepository.GetExhibitOnExhibitionById(exhibitionId);
        }

        public async Task<Guid> AddExhibitToExhibition(Guid exhibitionId, Guid exhibitId)
        {
            return await _exhibitionRepository.AddExhibitToExhibition(exhibitionId, exhibitId);
        }

        public async Task<Guid> DeleteExhibitFromExhibition(Guid exhibitionId, Guid exhibitId)
        {
            return await _exhibitionRepository.DeleteExhibitFromExhibition(exhibitionId, exhibitId);
        }



    }
}
