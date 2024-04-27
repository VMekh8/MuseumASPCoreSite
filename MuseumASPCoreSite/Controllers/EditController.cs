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
    public class EditController : ControllerBase
    {

        private readonly IExhibitService _exhibitService;
        private readonly IExhibitionService _exhibitionService;
        private readonly IMuseumNewsService _museumNewsService;


        public EditController(IExhibitService exhibitService, IExhibitionService exhibitionService, IMuseumNewsService museumNewsService)
        {
            _exhibitService = exhibitService;
            _exhibitionService = exhibitionService;
            _museumNewsService = museumNewsService;
        }

        [HttpPut("ExhibitEdit{id:int}")]
        public async Task<ActionResult<int>> EditExhibit([FromForm] ExhibitRequest exhibitRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            byte[] filebytes;

            using (var ms = new MemoryStream())
            {
                await exhibitRequest.Image.CopyToAsync(ms);
                filebytes = ms.ToArray();
            }

            var (exhibit, error) = Exhibit.CreateExhibit(
                exhibitRequest.Id,
                exhibitRequest.Title,
                exhibitRequest.Description,
                filebytes
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            return await _exhibitService.UpdateExhibitAsync(exhibit) > 0 ?
                Ok(exhibit.Id) :
                BadRequest("Exhibit update error");
        }

        [HttpPut("ExhibitionEdit{id:int}")]
        public async Task<ActionResult<int>> EditExhibition([FromForm] ExhibitionRequest request)
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

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            return await _exhibitionService.UpdateExhibitionAsync(exhibition) > 0 ?
                Ok(exhibition.Id) :
                BadRequest("Exhibition update error");
        }

        [HttpPut("NewsEdit{id:int}")]
        public async Task<ActionResult<int>> MuseumNewsEdit([FromBody]MuseumNewsRequest request)
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

            var (news, error) = MuseumNews.CreateNews(
                request.Id,
                request.Title,
                request.Description,
                filebytes
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            return await _museumNewsService.UpdateNewsAsync(news) > 0 ?
                Ok(news.Id) :
                BadRequest("Museum news edit error");
        }
    }

}
