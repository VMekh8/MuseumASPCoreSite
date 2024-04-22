using Microsoft.EntityFrameworkCore;
using MuseumSite.Core.Abstract;
using MuseumSite.Core.Models;
using MuseumSite.Domain.Entitites;

namespace MuseumSite.Domain.Repository
{
    public class ExhibitRepository : IRepository<Exhibit>
    {
        private readonly MuseumDbContext _context;
        public ExhibitRepository(MuseumDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Exhibit entity)
        {
            var exhibitEntity = new ExhitbitEntity()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Image = entity.Image
            };

           await _context.ExhitbitEntity.AddAsync(exhibitEntity);
           await _context.SaveChangesAsync();

           return exhibitEntity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.ExhitbitEntity
                .Where(e => e.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<Exhibit>> GetAllItems()
        {
            var exhibitEntities = await _context.ExhitbitEntity.ToListAsync();

            var ExhibitItems = exhibitEntities
                                .Select(e => Exhibit.CreateExhibit(e.Id, e.Title, e.Description, e.Image).Exhibit)
                                .ToList();

            return ExhibitItems;
        }

        public async Task<Exhibit> GetItemById(Guid id)
        {
            var item = await _context.ExhitbitEntity.FirstOrDefaultAsync(e => e.Id == id);

            Exhibit exhibit = Exhibit.CreateExhibit(
                item.Id,
                item.Title,
                item.Description,
                item.Image
                ).Exhibit;

            return exhibit;
        }

        public async Task<Guid> Update(Exhibit entity)
        {
            await _context.ExhitbitEntity
                .Where(e => e.Id == entity.Id)
                .ExecuteUpdateAsync(e => e
                    .SetProperty(opt => opt.Title, entity.Title)
                    .SetProperty(opt => opt.Description, entity.Description)
                    .SetProperty(opt => opt.Image, entity.Image)
                );
            
            return entity.Id;
        }
    }
}
