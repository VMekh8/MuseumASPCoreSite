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
        public async Task<ActionResult<int>> EditExhibit([FromForm]ExhibitRequest exhibitRequest)
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

    }
}
