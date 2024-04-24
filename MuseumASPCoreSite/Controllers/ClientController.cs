using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumASPCoreSite.Contracts;
using MuseumSite.Application.Services;

namespace MuseumASPCoreSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles ="Client, Worker, Admin")]
    public class ClientController : ControllerBase
    {
        private readonly IExhibitService _exhibitService;
        private readonly IExhibitionService _exhibitionService;
        private readonly IMuseumNewsService _museumNewsService;


        public ClientController(IExhibitService exhibitService, IExhibitionService exhibitionService, IMuseumNewsService museumNewsService)
        {
            _exhibitService = exhibitService;
            _exhibitionService = exhibitionService;
            _museumNewsService = museumNewsService;
        }

        [HttpGet("GetAllExhibit")]
        public async Task<ActionResult<List<ExhibitResponce>>> GetAllExhibits()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exhibitsModel = await _exhibitService.GetExhibitsAsync();

            if (exhibitsModel.Count <= 0)
            {
                return Ok("Exhibits collection is empty");
            }

            if (exhibitsModel.Count > 0)
            {
                var exhibitToSend = exhibitsModel.Select(e => new ExhibitResponce(
                    e.Id,
                    e.Title,
                    e.Description,
                    Convert.ToBase64String(e.Image),
                    e.ExhibitionId));

                return Ok(exhibitToSend);
            }

            return BadRequest("Exhibits get error");
        }
    }
}
