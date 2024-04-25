using MuseumSite.Core.Models;

namespace MuseumSite.Application.Services
{
    public interface IExhibitionService
    {
        Task<int> AddExhibitToExhibition(int exhibitionId, int exhibitId);
        Task<int> CreateExhibitionAsync(Exhibition exhibition);
        Task<int> DeleteExhibitFromExhibition(int exhibitionId, int exhibitId);
        Task<int> DeleteExhibitionAsync(int id);
        Task<Exhibition> GetExhibitionByNameAsync(string name);
        Task<List<Exhibition>> GetExhibitionsAsync();
        Task<List<Exhibit>> GetExhibitsOnExhibitionAsync(string name);
        Task<int> UpdateExhibitionAsync(Exhibition exhibition);
    }
}