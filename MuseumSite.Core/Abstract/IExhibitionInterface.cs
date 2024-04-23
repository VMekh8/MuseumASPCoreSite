using MuseumSite.Core.Models;

namespace MuseumSite.Core.Abstract
{
    public interface IExhibitionInterface : IRepository<Exhibition>
    {
        Task<List<Exhibit>> GetExhibitOnExhibitionById(Guid exhibitionId);
        Task<Guid> AddExhibitToExhibition(Guid exhibitionId, Guid exhibitId);
        Task<Guid> DeleteExhibitFromExhibition(Guid exhibitionId, Guid exhibitId);
    }
}
