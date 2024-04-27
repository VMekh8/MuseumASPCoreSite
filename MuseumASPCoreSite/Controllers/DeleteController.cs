using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumSite.Application.Services;

namespace MuseumASPCoreSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles ="Worker, Admin")]
    public class DeleteController : ControllerBase
    {
        private readonly IExhibitService _exhibitService;
        private readonly IExhibitionService _exhibitionService;
        private readonly IMuseumNewsService _museumNewsService;


        public DeleteController(IExhibitService exhibitService, IExhibitionService exhibitionService, IMuseumNewsService museumNewsService)
        {
            _exhibitService = exhibitService;
            _exhibitionService = exhibitionService;
            _museumNewsService = museumNewsService;
        }

        [HttpDelete("DeleteExhibit{id:int}")]
        public async Task<ActionResult<int>> DeleteExhibit(int id)
        {
            var result = await _exhibitService.DeleteExhibitAsync(id);

            if (result > 0)
            {
                return Ok(result);
            }

            return BadRequest("Exhibit delete error");
        }

        [HttpDelete("DeleteExhibition{id:int}")]
        public async Task<ActionResult<int>> DeleteExhibition(int id)
        {
            return await _exhibitionService.DeleteExhibitionAsync(id) > 0 ?
                 Ok(await _exhibitionService.DeleteExhibitionAsync(id)) :
                 BadRequest("Exhibition delete error");            
        }
    }
}
