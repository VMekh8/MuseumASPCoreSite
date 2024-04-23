using MuseumSite.Core.Models;
using MuseumSite.Domain.Repository;

namespace MuseumSite.Application.Services
{
    public class ExhibitionService
    {
        private readonly ExhibitionRepository _exhibitionRepository;

        public ExhibitionService(ExhibitionRepository exhibitionRepository)
        {
            _exhibitionRepository = exhibitionRepository;
        }

       
    }
}
