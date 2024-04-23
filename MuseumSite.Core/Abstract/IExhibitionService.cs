using MuseumSite.Core.Models;

namespace MuseumSite.Application.Services
{
    public interface IExhibitionService
    {
        Task<Guid> AddExhibitToExhibition(Guid exhibitionId, Guid exhibitId);
        Task<Guid> CreateExhibitionAsync(Exhibition exhibition);
        Task<Guid> DeleteExhibitFromExhibition(Guid exhibitionId, Guid exhibitId);
        Task<Guid> DeleteExhibitionAsync(Guid id);
        Task<Exhibition> GetExhibitionByIdAsync(Guid id);
        Task<List<Exhibition>> GetExhibitionsAsync();
        Task<List<Exhibit>> GetExhibitsOnExhibitionAsync(Guid exhibitionId);
        Task<Guid> UpdateExhibitionAsync(Exhibition exhibition);
    }
}