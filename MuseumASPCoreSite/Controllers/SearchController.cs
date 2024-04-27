﻿using Microsoft.AspNetCore.Mvc;
using MuseumASPCoreSite.Contracts;
using MuseumSite.Application.Services;
using MuseumSite.Core.Abstract;
using MuseumSite.Core.Models;

namespace MuseumASPCoreSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IExhibitService _exhibitService;
        private readonly IExhibitionService _exhibitionService;
        private readonly IMuseumNewsService _newsService;

        public SearchController(IExhibitService exhibitService, IExhibitionService exhibitionService, IMuseumNewsService newsService)
        {
            _exhibitService = exhibitService;
            _exhibitionService = exhibitionService;
            _newsService = newsService;
        }

        [HttpGet("GetExhibitByName")]
        public async Task<ActionResult<ExhibitResponce>> GetExhibitByName(string name)
        {
            var found = await _exhibitService.GetExhibitByNameAsync(name);
            if (found == null)
            {
                return NotFound();
            }

            return Ok(new ExhibitResponce(
                found.Id,
                found.Title,
                found.Description,
                Convert.ToBase64String(found.Image),
                found.ExhibitionId
                ));
        }

        [HttpGet("GetExhibitionByName")]
        public async Task<ActionResult<ExhibitionResponce>> GetExhibitionByName(string name)
        {
            var found = await _exhibitionService.GetExhibitionByNameAsync(name);
            if (found == null)
            {
                return NotFound();
            }

            var exhibits = found.Exhibits.Select(e => new ExhibitResponce(e.Id, e.Title, e.Description, Convert.ToBase64String(e.Image), e.ExhibitionId)).ToList();

            return Ok(new ExhibitionResponce(
                found.Id,
                found.Name,
                found.Description,
                Convert.ToBase64String(found.Image),
                found.EventDate,
                exhibits
            ));
        }

    }
}
