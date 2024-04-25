using Microsoft.AspNetCore.Mvc;
using MuseumSite.Application.Services;

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
        

        //[HttpPost]
        //public async Task<ActionResult<Guid>> CreateExhibit([FromBody])
    }
}
