using MuseumSite.Core.Models;

namespace MuseumSite.Application.Services
{
    public interface IExhibitService
    {
        Task<Guid> CreateExhibitAsync(Exhibit exhibit);
        Task<Guid> DeleteExhibitAsync(Guid id);
        Task<Exhibit> GetExhibitByNameAsync(string name);
        Task<List<Exhibit>> GetExhibitsAsync();
        Task<Guid> UpdateExhibitAsync(Exhibit exhibit);
    }
}