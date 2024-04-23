using MuseumSite.Core.Abstract;
using MuseumSite.Core.Models;
using MuseumSite.Domain.Repository;

namespace MuseumSite.Application.Services
{
    public class ExhibitService : IExhibitService
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

        public async Task<Guid> CreateExhibitAsync(Exhibit exhibit)
        {
            return await _exhibitRepository.Create(exhibit);
        }

        public async Task<Guid> DeleteExhibitAsync(Guid id)
        {
            return await _exhibitRepository.Delete(id);
        }

        public async Task<Exhibit> GetExhibitByIdAsync(Guid id)
        {
            return await _exhibitRepository.GetItemById(id);
        }

        public async Task<Guid> UpdateExhibitAsync(Exhibit exhibit)
        {
            return await _exhibitRepository.Update(exhibit);
        }
    }
}
