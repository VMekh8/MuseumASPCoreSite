using MuseumSite.Core.Abstract;
using MuseumSite.Core.Models;
using MuseumSite.Domain.Repository;

namespace MuseumSite.Application.Services
{
    public class ExhibitService
    {
        private readonly ExhibitRepository _exhibitRepository;

        public ExhibitService(ExhibitRepository exhibitService)
        {
            _exhibitRepository = exhibitService;
        }

        public async Task<List<Exhibit>> GetExhibitsAsync() 
        {
            return await _exhibitRepository.GetAllItems();
        }

        public async Task<Guid> CreateExhibit(Exhibit exhibit)
        {
            return await _exhibitRepository.Create(exhibit);
        }

        public async Task<Guid> DeleteExhibit(Guid id)
        {
            return await _exhibitRepository.Delete(id);
        }

        public async Task<Exhibit> GetExhibitById(Guid id)
        {
            return await _exhibitRepository.GetItemById(id);
        }

        public async Task<Guid> UpdateExhibit(Exhibit exhibit)
        {
            return await _exhibitRepository.Update(exhibit);
        }
    }
}
