using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumASPCoreSite.Contracts;
using MuseumSite.Application.Services;
using MuseumSite.Core.Models;

namespace MuseumASPCoreSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IExhibitService _exhibitService;

        public AdminController(IExhibitService exhibitService)
        {
            _exhibitService = exhibitService;
        }

       
        
    }
}
