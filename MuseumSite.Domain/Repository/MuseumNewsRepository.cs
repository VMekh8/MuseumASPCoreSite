using Microsoft.EntityFrameworkCore;
using MuseumSite.Core.Abstract;
using MuseumSite.Core.Models;
using MuseumSite.Domain.Entitites;

namespace MuseumSite.Domain.Repository
{
    public class MuseumNewsRepository : IRepository<MuseumNews>
    {
        private readonly MuseumDbContext _context;

        public MuseumNewsRepository(MuseumDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(MuseumNews entity)
        {
            var MuseumNewsEntity = new MuseumNewsEntity()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Image = entity.Image
            };

            await _context.MuseumNewsEntity.AddAsync(MuseumNewsEntity);
            await _context.SaveChangesAsync();

            return MuseumNewsEntity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.MuseumNewsEntity
                .Where(opt => opt.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<MuseumNews>> GetAllItems()
        {
            var MuseumNewsEntities = await _context.MuseumNewsEntity.ToListAsync();

            var MuseumNewsItems = MuseumNewsEntities
                .Select(opt => MuseumNews.CreateNews(opt.Id, opt.Title, opt.Description, opt.Image).News)
                .ToList();

            return MuseumNewsItems;
        }

        public async Task<MuseumNews> GetItemByName(string name)
        {
            var found = await _context.MuseumNewsEntity.FirstOrDefaultAsync(opt => opt.Title == name);

            var News = MuseumNews.CreateNews(
                found.Id,
                found.Title,
                found.Description,
                found.Image
                ).News;

            return News;
        }

        public async Task<Guid> Update(MuseumNews entity)
        {
            await _context.MuseumNewsEntity
                .Where(opt => opt.Id == entity.Id)
                .ExecuteUpdateAsync(e => e
                .SetProperty(e => e.Title, entity.Title)
                .SetProperty(e => e.Description, entity.Description)
                .SetProperty(e => e.Image, entity.Image)
                );

            return entity.Id;
        }
    }
}
