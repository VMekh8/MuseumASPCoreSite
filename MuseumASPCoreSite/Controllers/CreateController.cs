using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumASPCoreSite.Contracts.Requests;
using MuseumSite.Application.Services;
using MuseumSite.Core.Models;

namespace MuseumASPCoreSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles ="Worker, Admin")]
    public class CreateController : ControllerBase
    {
        private readonly IExhibitService _exhibitService;
        private readonly IExhibitionService _exhibitionService;
        private readonly IMuseumNewsService _museumNewsService;


        public CreateController(IExhibitService exhibitService, IExhibitionService exhibitionService, IMuseumNewsService museumNewsService)
        {
            _exhibitService = exhibitService;
            _exhibitionService = exhibitionService;
            _museumNewsService = museumNewsService;


        }


        [HttpPost("CreateExhibit")]
        public async Task<ActionResult<int>> CreateExhibit([FromForm] ExhibitRequest exhibitRequest)
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

            if (exhibit == null)
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


        [HttpPost("CreateExhibition")]
        public async Task<ActionResult<int>> CreateExhibition([FromForm] ExhibitionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            byte[] filebytes;

            using (var ms = new MemoryStream())
            {
                await request.Image.CopyToAsync(ms);
                filebytes = ms.ToArray();
            }

            var (exhibition, error) = Exhibition.CreateExhibition(
                request.Id,
                request.Name,
                request.Description,
                request.EventDate,
                filebytes
                );

            if (!string.IsNullOrWhiteSpace(error))
            {
                return BadRequest(error);
            }

            if (exhibition == null || exhibition.Id == 0)
            {
                return BadRequest("Error to exhibition init");
            }

            var result = await _exhibitionService.CreateExhibitionAsync(exhibition);

            if (result > 0)
            {
                return Ok(result);
            }

            return BadRequest("Exhibition create error");
        }

        [HttpPost("CreateNews")]
        public async Task<ActionResult<int>> CreateNews([FromForm]MuseumNewsRequest museumNews)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            byte[] filebytes;
            using (var ms = new MemoryStream())
            {
                await museumNews.Image.CopyToAsync(ms);
                filebytes = ms.ToArray();
            }

            var (News, error) = MuseumNews.CreateNews(
                museumNews.Id,
                museumNews.Title,
                museumNews.Description,
                filebytes
                );

            if(!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }    

            if (News == null || News.Id == 0)
            {
                return BadRequest("Museum News init error");
            }

            var result = await _museumNewsService.CreateNewsAsync(News);

            if (result > 0)
            {
                return Ok(result);
            }

            return BadRequest("Museum news create error");
        }


    }
}
