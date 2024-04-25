using MuseumSite.Core.Models;

namespace MuseumSite.Application.Services
{
    public interface IExhibitService
    {
        Task<int> CreateExhibitAsync(Exhibit exhibit);
        Task<int> DeleteExhibitAsync(int id);
        Task<Exhibit> GetExhibitByNameAsync(string name);
        Task<List<Exhibit>> GetExhibitsAsync();
        Task<int> UpdateExhibitAsync(Exhibit exhibit);
    }
}