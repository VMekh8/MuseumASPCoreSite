using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumASPCoreSite.Contracts;
using MuseumASPCoreSite.Contracts.Requests;
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

        [HttpGet("GetAllExhibitions")]
        public async Task<ActionResult<List<ExhibitionResponce>>> GetAllExhibitions()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exhibitionsModel = await _exhibitionService.GetExhibitionsAsync();

            if (exhibitionsModel.Count <= 0)
            {
                return Ok("Exhibitions collection is empty");
            }

            if (exhibitionsModel.Count > 0)
            {
                var exhibitionsToSend = exhibitionsModel.Select(e =>
                {
                    var exhibitResponses = e.Exhibits.Select(exhibit => new ExhibitResponce(
                        exhibit.Id,
                        exhibit.Title,
                        exhibit.Description,
                        Convert.ToBase64String(exhibit.Image),
                        exhibit.ExhibitionId
                    )).ToList();

                    return new ExhibitionResponce(
                        e.Id,
                        e.Name,
                        e.Description,
                        Convert.ToBase64String(e.Image),
                        exhibitResponses
                    );
                });

                return Ok(exhibitionsToSend);
            }


            return BadRequest("Exhibitions get error");
        }

        [HttpGet("GetAllNews")]
        public async Task<ActionResult<List<MuseumNewsResponce>>> GetAllNews()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newsModel = await _museumNewsService.GetAllNewsAsync();

            if (newsModel.Count <= 0)
            {
                return Ok("Museum News collection is empty");
            }

            

            if (newsModel.Count > 0)
            {
                var newsToSend = newsModel.Select(e => new MuseumNewsResponce(
                    e.Id,
                    e.Title,
                    e.Description,
                    Convert.ToBase64String(e.Image)
                    ));

                return Ok(newsToSend);
            }

            return BadRequest("Museum News get error");
        }
    }
}
