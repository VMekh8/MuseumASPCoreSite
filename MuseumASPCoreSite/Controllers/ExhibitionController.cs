using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Worker, Admin")]

    public class ExhibitionController : ControllerBase
    {
        private readonly IExhibitionService _exhibitionService;
        private readonly IExhibitService _exhibitService;

        public ExhibitionController(IExhibitService exhibitService, IExhibitionService exhibitionService)
        {
            _exhibitService = exhibitService;
            _exhibitionService = exhibitionService;
        }

        [HttpGet("GetExhibitsOnExhibitions")]
        public async Task<ActionResult<List<ExhibitResponce>>> GetExhibitsOnExhibition([FromForm] string name)
        {
            var exhibitsModel = await _exhibitionService.GetExhibitsOnExhibitionAsync(name);

            var exhibitsOnExhibition = exhibitsModel.Select(
                e => new ExhibitResponce(e.Id, e.Title, e.Description, Convert.ToBase64String(e.Image), e.ExhibitionId));
            
            return Ok(exhibitsOnExhibition);
        }

        [HttpPost("AddExhibitToExhibition")]
        public async Task<ActionResult<int>> AddExhibitToExhibition([FromForm] ExhibitExhibitionRequest request)
        {
            return await _exhibitionService.AddExhibitToExhibition(request.exhibitionId, request.exhibitId) > 1 ?
                Ok(await _exhibitionService.AddExhibitToExhibition(request.exhibitionId, request.exhibitId)) :
                BadRequest("Error of adding an exhibit to the exhibition");
        }

        [HttpDelete("DeleteExhibitToExhibition")]
        public async Task<ActionResult<int>> DeleteExhibitToExhibition([FromForm] ExhibitExhibitionRequest request)
        {
            return await _exhibitionService.DeleteExhibitFromExhibition(request.exhibitionId, request.exhibitId) > 1 ?
                Ok(await _exhibitionService.DeleteExhibitFromExhibition(request.exhibitionId, request.exhibitId)) :
                BadRequest("Error of deleting an exhibit to the exhibition");
        }
    }
}
