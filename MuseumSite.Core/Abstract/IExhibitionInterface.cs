using MuseumSite.Core.Models;

namespace MuseumSite.Core.Abstract
{
    public interface IExhibitionInterface : IRepository<Exhibition>
    {
        Task<List<Exhibit>> GetExhibitOnExhibitionByName(string name);
        Task<int> AddExhibitToExhibition(int exhibitionId, int exhibitId);
        Task<int> DeleteExhibitFromExhibition(int exhibitionId, int exhibitId);
    }
}
