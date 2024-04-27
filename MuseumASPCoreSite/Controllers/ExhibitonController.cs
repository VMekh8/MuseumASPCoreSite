using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuseumASPCoreSite.Contracts;
using MuseumASPCoreSite.Contracts.Requests;
using MuseumSite.Application.Services;
using MuseumSite.Core.Models;

namespace MuseumASPCoreSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExhibitonController : ControllerBase
    {
        private readonly IExhibitionService _exhibitionService;
        private readonly IExhibitService _exhibitService;

        public ExhibitonController(IExhibitService exhibitService, IExhibitionService exhibitionService)
        {
            _exhibitService = exhibitService;
            _exhibitionService = exhibitionService;
        }

        

        [HttpPost("AddExhibitToExhibition")]
        public async Task<ActionResult<int>> AddExhibitToExhibition(int exhibitionId, int exhibitId)
        {
            return await _exhibitionService.AddExhibitToExhibition(exhibitionId, exhibitId);
        }

        [HttpDelete("DeleteExhibitToExhibition")]
        public async Task<ActionResult<int>> DeleteExhibitToExhibition(int exhibitionId, int exhibitId)
        {
            return await _exhibitionService.DeleteExhibitFromExhibition(exhibitionId, exhibitId);
        }
    }
}
