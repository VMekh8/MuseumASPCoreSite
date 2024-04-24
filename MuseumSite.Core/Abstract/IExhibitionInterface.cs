using MuseumSite.Core.Models;

namespace MuseumSite.Core.Abstract
{
    public interface IExhibitionInterface : IRepository<Exhibition>
    {
        Task<List<Exhibit>> GetExhibitOnExhibitionByName(string name);
        Task<Guid> AddExhibitToExhibition(Guid exhibitionId, Guid exhibitId);
        Task<Guid> DeleteExhibitFromExhibition(Guid exhibitionId, Guid exhibitId);
    }
}
