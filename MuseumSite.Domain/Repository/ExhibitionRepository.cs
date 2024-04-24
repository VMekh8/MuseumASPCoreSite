using Microsoft.EntityFrameworkCore;
using MuseumSite.Core.Abstract;
using MuseumSite.Core.Models;
using MuseumSite.Domain.Entitites;

namespace MuseumSite.Domain.Repository
{
    public class ExhibitionRepository : IExhibitionInterface
    {
        private readonly MuseumDbContext _context;

        public ExhibitionRepository(MuseumDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Create(Exhibition entity)
        {
            var exhibitionEntity = new ExhibitionEntity
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Image = entity.Image,
                EventDate = entity.EventDate
            };

            await _context.ExhibitionEntity.AddAsync(exhibitionEntity);
            await _context.SaveChangesAsync();

            return exhibitionEntity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.ExhibitionEntity
                .Where(e => e.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<List<Exhibition>> GetAllItems()
        {
            var exhibitionEntitites = await _context.ExhibitionEntity.ToListAsync();

            var exhibitionItems = exhibitionEntitites
                .Select(e => Exhibition.CreateExhibition(e.Id, e.Name, e.Description, e.EventDate, e.Image).Exhibition)
                .ToList();

            return exhibitionItems;
        }

        public async Task<Exhibition> GetItemByName(string name)
        {
            var item = await _context.ExhibitionEntity.FirstOrDefaultAsync(e => e.Name == name);

            Exhibition exhibition = Exhibition.CreateExhibition(
                item.Id,
                item.Name,
                item.Description,
                item.EventDate,
                item.Image
                ).Exhibition;

            return exhibition;
        }

        public async Task<Guid> Update(Exhibition entity)
        {
            await _context.ExhibitionEntity
                .Where(e => e.Id == entity.Id)
                .ExecuteUpdateAsync(e => e
                    .SetProperty(opt => opt.Name, entity.Name)
                    .SetProperty(opt => opt.Description, entity.Description)
                    .SetProperty(opt => opt.EventDate, entity.EventDate)
                    .SetProperty(opt => opt.Image, entity.Image)
                );

            return entity.Id;
        }

        public async Task<List<Exhibit>> GetExhibitOnExhibitionByName(string name)
        {
            var exhibition = await _context.ExhibitionEntity
                   .Include(e => e.ExhitbitsEntities)
                   .FirstOrDefaultAsync(e => e.Name == name);

            var exhibits = exhibition?.ExhitbitsEntities
                .Select(e => Exhibit.CreateExhibit
            (
                e.Id,
                e.Title,
                e.Description,
                e.Image
            ).Exhibit).ToList();

            return exhibits;
        }


        public async Task<Guid> AddExhibitToExhibition(Guid exhibitionId, Guid exhibitId)
        {
            var exhibition = await _context.ExhibitionEntity
                   .Include(e => e.ExhitbitsEntities)
                   .FirstOrDefaultAsync(e => e.Id == exhibitionId);

            var exhibitEntity = await _context.ExhitbitEntity
                .FirstOrDefaultAsync(e => e.Id == exhibitId);

            if (exhibition != null && exhibitEntity != null)
            {
                exhibition.ExhitbitsEntities.Add(exhibitEntity);
                await _context.SaveChangesAsync();
            }

            return exhibitId;
        }


        public async Task<Guid> DeleteExhibitFromExhibition(Guid exhibitionId, Guid exhibitId)
        {
            var exhibition = await _context.ExhibitionEntity
                   .Include(e => e.ExhitbitsEntities)
                   .FirstOrDefaultAsync(e => e.Id == exhibitionId);

            var exhibitEntity = await _context.ExhitbitEntity
                .FirstOrDefaultAsync(e => e.Id == exhibitId);

            if (exhibition != null && exhibitEntity != null)
            {
                exhibition.ExhitbitsEntities.Remove(exhibitEntity);
                await _context.SaveChangesAsync();
            }

            return exhibitId;
        }


    }
}

