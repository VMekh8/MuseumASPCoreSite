using Microsoft.AspNetCore.Mvc;
using MuseumASPCoreSite.Contracts.Requests;
using MuseumSite.Application.Services;
using MuseumSite.Core.Models;

namespace MuseumASPCoreSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles ="Client, Worker, Admin")]
    public class WorkerController : ControllerBase
    {
        private readonly IExhibitService _exhibitService;
        private readonly IExhibitionService _exhibitionService;
        private readonly IMuseumNewsService _museumNewsService;


        public WorkerController(IExhibitService exhibitService, IExhibitionService exhibitionService, IMuseumNewsService museumNewsService)
        {
            _exhibitService = exhibitService;
            _exhibitionService = exhibitionService;
            _museumNewsService = museumNewsService;

            
        }
        

        [HttpPost]
        public async Task<ActionResult<int>> CreateExhibit([FromForm]ExhibitRequest exhibitRequest)
        {

            if (exhibitRequest == null)
            {
                return BadRequest("NullRequestError");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await exhibitRequest.Image.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            var (exhibit, error) = Exhibit.CreateExhibit(
                exhibitRequest.Id,
                exhibitRequest.Title,
                exhibitRequest.Description,
                fileBytes
                );

            if (!string.IsNullOrWhiteSpace(error))
            {
                return BadRequest(error);
            }

            if( exhibit == null)
            {
                return BadRequest("ExhibitNullError");
            }

            var result = await _exhibitService.CreateExhibitAsync(exhibit);

            if (result > 0)
            {
                return Ok(result);
            }

            return BadRequest("Exhibit create error");
        }
    }
}
