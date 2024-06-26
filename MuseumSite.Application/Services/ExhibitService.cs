﻿using MuseumSite.Core.Abstract;
using MuseumSite.Core.Models;
using MuseumSite.Domain.Repository;

namespace MuseumSite.Application.Services
{
    public class ExhibitService : IExhibitService
    {
        private readonly IRepository<Exhibit> _exhibitRepository;

        public ExhibitService(IRepository<Exhibit> exhibitService)
        {
            _exhibitRepository = exhibitService;
        }

        public async Task<List<Exhibit>> GetExhibitsAsync()
        {
            return await _exhibitRepository.GetAllItems();
        }

        public async Task<int> CreateExhibitAsync(Exhibit exhibit)
        {
            return await _exhibitRepository.Create(exhibit);
        }

        public async Task<int> DeleteExhibitAsync(int id)
        {
            return await _exhibitRepository.Delete(id);
        }

        public async Task<Exhibit> GetExhibitByNameAsync(string name)
        {
            return await _exhibitRepository.GetItemByName(name);
        }

        public async Task<int> UpdateExhibitAsync(Exhibit exhibit)
        {
            return await _exhibitRepository.Update(exhibit);
        }
    }
}
